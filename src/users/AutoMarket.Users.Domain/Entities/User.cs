using AutoMarket.Infrastructure.Entities;
using AutoMarket.Users.Domain.ValueObjects;
using ErrorOr;

namespace AutoMarket.Users.Domain.Entities;

public class User : Entity<Guid>
{
    private User() { }

    private User(FullName fullName, Email email, Password password, Address address)
    {
        FullName = fullName;
        Email = email;
        Password = password;
        Address = address;
    }

    public FullName FullName { get; private set; } = null!;

    public Email Email { get; private set; } = null!;

    public Password Password { get; private set; } = null!;

    public Address Address { get; private set; } = null!;

    public static ErrorOr<User> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string street,
        string number,
        string city,
        string country)
    {
       if(string.IsNullOrWhiteSpace(firstName))
           return Error.Validation(
               nameof(firstName),
               "First name cannot be empty");

        if(string.IsNullOrWhiteSpace(lastName))
            return Error.Validation(
                nameof(lastName),
                "Last name cannot be empty");

        if(string.IsNullOrWhiteSpace(email))
            return Error.Validation(
                nameof(email),
                "Email cannot be empty");

        if(string.IsNullOrWhiteSpace(password))
            return Error.Validation(
                nameof(password),
                "Password cannot be empty");

        if(string.IsNullOrWhiteSpace(street))
            return Error.Validation(
                nameof(street),
                "Street cannot be empty");

        if(string.IsNullOrWhiteSpace(number))
            return Error.Validation(
                nameof(number),
                "Number cannot be empty");

        if(string.IsNullOrWhiteSpace(city))
            return Error.Validation(
                nameof(city),
                "City cannot be empty");

        if(string.IsNullOrWhiteSpace(country))
            return Error.Validation(
                nameof(country),
                "Country cannot be empty");

        return new User(
            FullName.Create(firstName, lastName),
            Email.Create(email),
            Password.Create(password),
            Address.Create(street, number, city, country));
    }

    public void SetFullName(string firstName, string lastName)
    {
        if(string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName));

        FullName = FullName.Create(firstName, lastName);
    }

    public void SetEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        Email = Email.Create(email);
    }

    public void SetPassword(string password)
    {
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException(nameof(password));

        Password = Password.Create(password);
    }

    public void SetAddress(
        string street,
        string number,
        string city,
        string country)
    {
        if(string.IsNullOrWhiteSpace(street))
            throw new ArgumentNullException(nameof(street));

        if(string.IsNullOrWhiteSpace(number))
            throw new ArgumentNullException(nameof(number));

        if(string.IsNullOrWhiteSpace(city))
            throw new ArgumentNullException(nameof(city));

        if(string.IsNullOrWhiteSpace(country))
            throw new ArgumentNullException(nameof(country));

        Address = Address.Create(street, number, city, country);
    }
}