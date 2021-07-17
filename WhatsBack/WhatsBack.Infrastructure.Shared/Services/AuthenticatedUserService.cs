using Microsoft.AspNetCore.Http;
using WhatsBack.Application.Interfaces;

namespace WhatsBack.Infrastructure.Shared.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId { get; }
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) => UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;

    }
}
