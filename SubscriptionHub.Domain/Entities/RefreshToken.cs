using SubscriptionHub.Domain.Common;

namespace SubscriptionHub.Domain.Entities
{
    public class RefreshToken : Entity
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; } = string.Empty;
        public DateTime ExpireAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow >= ExpireAt;
        public bool IsActive => !IsRevoked && !IsExpired;
        private RefreshToken()
        {

        }

        public RefreshToken(Guid userId, string token, DateTime expireAt)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId is required.", nameof(userId));
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token is required.", nameof(token));
            if (expireAt == default)
                throw new ArgumentException("ExpireAt must be in the future.", nameof(expireAt));
            UserId = userId;
            Token = token;
            ExpireAt = expireAt;
            IsRevoked = false;
            CreatedAt = DateTime.UtcNow;
        }

        public void Revoke()
        {
            IsRevoked = true;
        }

    }
}
