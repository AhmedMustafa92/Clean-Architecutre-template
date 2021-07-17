using WhatsBack.Application.Interfaces;
using WhatsBack.Domain.Settings;
using WhatsBack.Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WhatsBack.Infrastructure.Shared.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings")); 
            services.AddTransient<IEmailService, EmailService>();
            // resolve DateTimeService
            ResolveDateTimeService(services);
        } 
    }
}
