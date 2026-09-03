namespace SubscriptionHub.Application.Auth.Commands.Login
{
    public record LoginResult
    {
        public string AccessToken { get; init; } = string.Empty;
        public string TokenType { get; init; } = "Bearer";
        public int ExpiresIn { get; init; }
        public Guid UserId { get; init; }
        public string Email { get; init; } = string.Empty;
        public string Role { get; init; } = string.Empty;
        public Guid TenantId { get; init; }
    }
}
