using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
          => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hash)
            => BCrypt.Net.BCrypt.Verify(password, hash);

    }
}
