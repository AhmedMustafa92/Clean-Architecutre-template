using Microsoft.AspNetCore.Http;

namespace WhatsBack.SharedKernal.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public string UserId { get; }
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor) => UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;

    }
}
