namespace AutoMarket.Users.Domain.ValueObjects;

public record Address
{
    private Address(string street, string number, string city, string country)
    {
        Street = street;
        Number = number;
        City = city;
        Country = country;
    }

    public string Street { get; init;}
    public string Number { get; init;}
    public string City { get; init;}
    public string Country { get; init;}

    public static Address Create(string street, string number, string city, string country)
    {
        return new Address(street, number, city, country);
    }
}