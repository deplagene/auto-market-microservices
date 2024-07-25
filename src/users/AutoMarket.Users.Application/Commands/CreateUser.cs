using AutoMarket.Infrastructure;
using AutoMarket.Infrastructure.Constants;
using AutoMarket.Infrastructure.Identity;
using AutoMarket.Users.Domain.Entities;
using AutoMarket.Users.Domain.Repositories;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace AutoMarket.Users.Application.Commands;

public class CreateUser
{
    public sealed record Request(
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string Street,
        string Number,
        string City,
        string Country) : IRequest<ErrorOr<User>>;

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator(IReadUserRepository repository)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email");

            RuleFor(x => x.Email)
                .MustAsync(repository.IsUniqueByEmailAsync)
                .WithMessage("Email already exists");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long");
        }
    }

    public sealed class Handler(
        IWriteUserRepository repository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IReadRolesRepository rolesRepository) : IRequestHandler<Request, ErrorOr<User>>
    {
        public async Task<ErrorOr<User>> Handle(Request request, CancellationToken cancellationToken)
        {
            var passwordHash = passwordHasher.Hash(request.Password);

            var role = await rolesRepository
                .GetByIdAsync(ApplicationUser.DefaultUser, cancellationToken);

            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                passwordHash,
                request.Street,
                request.Number,
                request.City,
                request.Country,
                [role]
            );

            if(user.IsError)
                return user.Errors;

            await repository.AddAsync(user.Value, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Value;
        }
    }
}