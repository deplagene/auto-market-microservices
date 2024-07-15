namespace AutoMarket.Users.Domain.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; init;}

    public string LastName { get; init;}

    public static FullName Create(string firstName, string lastName)
    {
        return new FullName(firstName, lastName);
    }
}