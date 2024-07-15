namespace AutoMarket.Cars.Domain.ValueObjects;

public record YearOfIssue
{
    private YearOfIssue(int value) => Value = value;
    public int Value { get; init;}

    public static YearOfIssue Create(int value)
    {
        return new YearOfIssue(value);
    }
}