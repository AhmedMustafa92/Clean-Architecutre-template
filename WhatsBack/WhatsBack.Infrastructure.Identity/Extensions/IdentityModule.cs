using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WhatsBack.Application.Interfaces;
using WhatsBack.Infrastructure.Identity.Contexts;
using WhatsBack.Infrastructure.Identity.Models;
using WhatsBack.Infrastructure.Identity.Services;

namespace WhatsBack.Infrastructure.Identity.Extensions
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterDbConext(builder);
            ResolveAccountService(builder);
        }

        private void RegisterDbConext(ContainerBuilder builder)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
                optionsBuilder.UseSqlServer(x.Resolve<IConfiguration>().GetConnectionString("IdentityConnection"));
                return new IdentityContext(optionsBuilder.Options);
            }).InstancePerLifetimeScope(); 
            builder.RegisterType<UserManager<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<RoleManager<IdentityRole>>().InstancePerRequest();
            builder.RegisterType<SignInManager<ApplicationUser>>().InstancePerRequest();
        }
        private static void ResolveAccountService(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>().PropertiesAutowired().InstancePerLifetimeScope();
        }

    }
}
