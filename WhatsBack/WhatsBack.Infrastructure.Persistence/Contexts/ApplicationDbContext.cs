using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace WhatsBack.Infrastructure.Persistence.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Fluent APIs
            base.OnModelCreating(builder);

            builder.BuildEnums();
            builder.SeedInitializer();
        }
    }
}
