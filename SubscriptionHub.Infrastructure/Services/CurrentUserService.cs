using Microsoft.AspNetCore.Http;
using SubscriptionHub.Application.Common.Interfaces;
using System.Security.Claims;

namespace SubscriptionHub.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User
                             .Claims
                             .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                 return Guid.TryParse(value, out var id) ? id : Guid.Empty;
            }
        }

        public Guid TenantId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User
                    .Claims
                    .FirstOrDefault(c => c.Type == "tenantId")?.Value;
                return Guid.TryParse(value, out var id) ? id : Guid.Empty ;
            }
        }

        public string Role
        {
            get
            {
                return _httpContextAccessor.HttpContext?.User
                    .Claims
                    .FirstOrDefault(c=>c.Type =="role")?.Value ?? string.Empty;
            }
        }
    }
}
