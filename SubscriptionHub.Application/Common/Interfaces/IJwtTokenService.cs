using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Common.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        int GetAccessTokenExpirationInMinutes();
    }
}
