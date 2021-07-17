using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhatsBack.Application.Interfaces;
using WhatsBack.Infrastructure.Shared.Services;
using WhatsBack.WebApi.Services;

namespace WhatsBack.WebApi
{
    public static class ServiceRegistration
    {
        public static void AddApiService(this IServiceCollection services, IConfiguration configuration)
        {
            
            // resolve AuthenticatedUserService
            ResolveAuthenticatedUserService(services);
        }

        private static void ResolveAuthenticatedUserService(IServiceCollection services)
        {
            services.AddSingleton(typeof(IAuthenticatedUserService),typeof(AuthenticatedUserService));
        }

        
    }
}
