using ErrorOr;

namespace AutoMarket.Cars.Domain.Errors;

public static class Errors
{
    public static class Cars
    {
        public static Error NotFound => Error.NotFound(
            code:"Car.NotFound",
            description:"Car not found");
    }

    public static class Brand
    {
        public static Error NotFound => Error.NotFound(
            code:"Brand.NotFound",
            description:"Brand not found");
    }
}