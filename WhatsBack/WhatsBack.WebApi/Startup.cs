using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhatsBack.Infrastructure.Identity.Extensions;
using WhatsBack.Application.Extensions;
using WhatsBack.Infrastructure.Persistence.Extensions;
using Autofac;
using WhatsBack.WebApi.Controllers;
using System;
using System.Linq;
using WhatsBack.Infrastructure.Shared.Extensions;
using WhatsBack.SharedKernal;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using WhatsBack.SharedKernal.Middlewares;
using WhatsBack.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace WhatsBack.WebApi
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add Controllers + As Service To Implment Property Injection
            services.AddControllersWithViews().AddControllersAsServices().AddNewtonsoftJson();
            #endregion
            services.AddIdentity<ApplicationUser, IdentityRole>();
            services.AddBasicServices(_config);  
            services.AddControllers();
            services.AddHealthChecks();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            #region Configure Modules
            builder.RegisterModule(new PersistenceModule());
            builder.RegisterModule(new SharedModule());
            builder.RegisterModule(new IdentityModule());
            builder.RegisterModule(new ApplicationModule());
            #endregion

            #region Register HttpContextAccessor In Order To Access The Http Context Inside A Class Library (Electricity.Correspondence.Core Project)
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance().PropertiesAutowired();
            #endregion 
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAppCustomMiddleware("client");
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
        }
    }
}
