using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using WhatsBack.Infrastructure.Identity.Contexts;
using Microsoft.EntityFrameworkCore;
using WhatsBack.Infrastructure.Persistence.Contexts;
using Autofac.Extensions.DependencyInjection;

namespace WhatsBack.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await EnableAutomaticMigration(host.Services.CreateScope());
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog() //Uses Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private async static Task EnableAutomaticMigration(IServiceScope scope)
        {
            using ( scope )
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    #region Enable Auto Migration fro identity context
                    var identityContext = services.GetRequiredService<IdentityContext>();
                    identityContext.Database.Migrate();
                    #endregion

                    #region Enable Auto Migration fro application context
                    var appContext = services.GetRequiredService<ApplicationDbContext>();
                    appContext.Database.Migrate();
                    #endregion

                    //initalize identity contex
                    await IdentityContextInitializer.SeedInitializer(services);

                    Log.Information("Application Starting");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }
    }
}
