using AutoMarket.Cars.Domain.Enums;
using AutoMarket.Cars.Domain.ValueObjects;
using AutoMarket.Infrastructure.Entities;
using ErrorOr;

namespace AutoMarket.Cars.Domain.Entities;

public class Car : Entity<Guid>
{
    private Car() { }

    private Car(string model, Price price, Guid brandId, CarType carType, string? description, YearOfIssue yearOfIssue, Guid ownerId)
    {
        Model = model;
        Price = price;
        BrandId = brandId;
        CarType = carType;
        Description = description;
        YearOfIssue = yearOfIssue;
        OwnerId = ownerId;
        CreatedAt = DateTime.UtcNow;
    }
    public string Model { get; private set; } = null!;

    public Price Price { get; private set; } = null!;

    public Guid BrandId { get; private set; }

    public Brand Brand { get; private set; } = null!;

    public CarType CarType { get; private set; }

    public string? Description { get; private set; }

    public YearOfIssue YearOfIssue { get; private set; } = null!;

    public DateTime CreatedAt { get; private set; }

    public Guid? OwnerId { get; private set; }

    public static ErrorOr<Car> Create(
        string model,
        decimal price,
        Guid brandId,
        CarType carType,
        string? description,
        int yearOfIssue,
        Guid ownerId)
    {
        if (string.IsNullOrWhiteSpace(model))
            return Error.Validation("Car.Model", "Car model is required");

        if (price <= 0)
            return Error.Validation("Car.Price", "Car price must be greater than 0");

        if (brandId == Guid.Empty)
            return Error.Validation("Car.BrandId", "Car brand is required");

        if (yearOfIssue <= 0)
            return Error.Validation("Car.YearOfIssue", "Car year of issue must be greater than 0");

        if (yearOfIssue < 1900)
            return Error.Validation("Car.YearOfIssue", "Car year of issue must be greater than 1900");

        if (yearOfIssue > DateTime.UtcNow.Year)
            return Error.Validation("Car.YearOfIssue", "Car year of issue must be less than current year");

        return new Car(model, Price.Create(price), brandId, carType, description, YearOfIssue.Create(yearOfIssue), ownerId);
    }
}