using Microsoft.EntityFrameworkCore;

namespace WhatsBack.Infrastructure.Shared.Context
{
    public class DbContextOptionsFactory<T> where T:DbContext
    {
        public static DbContextOptions<T> Get(string connectionString,bool useInMemoryDatabase)
        {
            var builder = new DbContextOptionsBuilder<T>();
            //DbContextConfigurer<T>.Configure( builder, connectionString);
            return builder.Options;
        }
    }
}
