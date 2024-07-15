namespace AutoMarket.Cars.Shared.Dtos;

public record BrandDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}