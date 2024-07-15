namespace AutoMarket.Cars.Domain.ValueObjects;

public record Price
{
    private Price(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; init;}

    public static Price Create(decimal value)
    {
        return new Price(value);
    }
}