using Autofac;
using Microsoft.AspNetCore.Http;
using WhatsBack.Application.Interfaces;
using WhatsBack.Infrastructure.Shared.Services;

namespace WhatsBack.Infrastructure.Shared.Extensions
{
    public class SharedModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterDateTimeService(builder);
            RegisterEmailService(builder);
            // resolve HttpContextAccessor
            ResolveHttpContextAccessor(builder);
            // resolve AuthenticatedUserService
            ResolveAuthenticatedUserService(builder);
        }
        private void RegisterDateTimeService(ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeService>().As<IDateTimeService>().PropertiesAutowired().InstancePerLifetimeScope();
        } 
        private void RegisterEmailService(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>().PropertiesAutowired().InstancePerLifetimeScope();
        }
        private static void ResolveHttpContextAccessor(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().PropertiesAutowired().InstancePerLifetimeScope();
        }
        private static void ResolveAuthenticatedUserService(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticatedUserService>().As<IAuthenticatedUserService>().PropertiesAutowired().InstancePerLifetimeScope();
        }
      
    }
}
