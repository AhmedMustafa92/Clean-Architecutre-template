using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WhatsBack.Infrastructure.Identity.Models;

namespace WhatsBack.Infrastructure.Identity.Contexts
{
    public partial class IdentityContext
    {
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<IdentityRole> Role { get; set; }
        public DbSet<IdentityUserRole<string>> UserRole { get; set; }
        public DbSet<IdentityUserLogin<string>> UserLogin { get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaim { get; set; }
        public DbSet<IdentityUserToken<string>> UserToken { get; set; }
    }
}
