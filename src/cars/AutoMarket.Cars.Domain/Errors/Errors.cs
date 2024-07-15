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
}