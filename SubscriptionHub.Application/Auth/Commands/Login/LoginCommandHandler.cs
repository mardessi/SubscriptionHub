using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Exceptions;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasher _passwordHasher;
       // private readonly JwtSettings _ jwtSettings;
        public LoginCommandHandler(IJwtTokenService jwtTokenService, IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            _jwtTokenService= jwtTokenService;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public async  Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email,cancellationToken);
            if (user == null || !_passwordHasher.Verify(request.Password,user.PasswordHash))
            {
                throw new UnauthorizedException();
            }
            var token = _jwtTokenService.GenerateAccessToken(user);
            return new LoginResult
            {
                AccessToken = token,
                Email = user.Email,
                UserId = user.Id,
                TenantId = user.TenantId,
                TokenType = "Bearer",
                Role = user.Role.ToString(),
                ExpiresIn = _jwtTokenService.GetAccessTokenExpirationInMinutes()

            };
        }
    }
}
