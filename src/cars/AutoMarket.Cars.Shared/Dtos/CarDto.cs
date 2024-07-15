namespace AutoMarket.Cars.Shared.Dtos;

public record CarDto
{
    public Guid Id { get; init; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public int YearOfIssue { get; init; }
    public int Price { get; init; }
}