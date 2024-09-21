namespace AutoMarket.Users.Domain.ValueObjects;

// TODO: add manual password hashing and salt

public record Password
{
    private Password(string value)
    {
        Value = value;
    }

    public string Value { get; init; }

    public static Password Create(string value)
    {
        return new Password(value);
    }
}