using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using WhatsBack.Infrastructure.Identity.Models;

namespace WhatsBack.Infrastructure.Identity.Contexts
{
    public static class IdentityContextInitializer
    {
        public async static Task SeedInitializer(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
            await Seeds.DefaultSuperAdmin.SeedAsync(userManager, roleManager);
            await Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
        }
    }
}
