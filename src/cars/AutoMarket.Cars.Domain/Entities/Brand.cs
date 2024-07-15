using AutoMarket.Infrastructure.Entities;
using ErrorOr;

namespace AutoMarket.Cars.Domain.Entities;

public class Brand : Entity<Guid>
{
    private readonly HashSet<Car> cars = new();
    private Brand() { }
    private Brand(string name)
    {
        Name = name;
    }

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<Car> Cars => cars;

    public static ErrorOr<Brand> Create(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Error.Validation("Brand.Name", "Brand name is required");

        return new Brand(name);
    }

    public void AddCar(Car car) => cars.Add(car);

    public void RemoveCar(Car car) => cars.Remove(car);
}