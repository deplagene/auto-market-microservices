using AutoMarket.Infrastructure.Entities;

namespace AutoMarket.Users.Domain.Entities;

public class Role : Entity
{
    private readonly HashSet<User> users = new();

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<User> Users => users;

    private Role() { }
    private Role(string name)
    {
        Name = name;
    }

    public static Role Create(string name) => new(name);
}