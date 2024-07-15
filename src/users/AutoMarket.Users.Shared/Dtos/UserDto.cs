namespace AutoMarket.Users.Shared.Dtos;

public sealed record UserDto
{
    public string Id { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
}