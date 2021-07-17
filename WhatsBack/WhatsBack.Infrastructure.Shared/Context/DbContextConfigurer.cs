using Microsoft.EntityFrameworkCore;

namespace WhatsBack.Infrastructure.Shared.Context
{
    public class DbContextConfigurer<T> where T : DbContext
    {
        public static void Configure(DbContextOptionsBuilder<T> builder, string connectionString, bool useInMemoryDatabase, string inMemoryDatabaseName)
        {

            //if (useInMemoryDatabase)
            //{
            //    builder.UseInMemoryDatabase(inMemoryDatabaseName);
            //}
            //else
            //{

            //    builder.(connectionString).UseLazyLoadingProxies();
            //}

        }
    }
}
