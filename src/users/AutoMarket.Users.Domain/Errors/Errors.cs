using ErrorOr;

namespace AutoMarket.Users.Domain.Errors;

public static class Errors
{
    public static class Users
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User not found");
    }
}