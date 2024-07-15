namespace AutoMarket.Users.Domain.ValueObjects;

public record Email
{
    private Email(string value)
    {
        Value = value;
        NormalizedValue = value.Normalize().ToUpper();
    }

    public string Value { get; init;}

    public string NormalizedValue { get; init;}

    public static Email Create(string value)
    {
        return new Email(value);
    }
}