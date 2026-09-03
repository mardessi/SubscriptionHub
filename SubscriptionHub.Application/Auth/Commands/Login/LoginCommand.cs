using MediatR;

namespace SubscriptionHub.Application.Auth.Commands.Login
{
    public record LoginCommand : IRequest<LoginResult>
    {
        public string Email { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
