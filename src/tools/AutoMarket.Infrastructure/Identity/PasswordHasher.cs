using BCrypt.Net;

namespace AutoMarket.Infrastructure.Identity;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    public bool Verify(string hash, string password) => BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
}
