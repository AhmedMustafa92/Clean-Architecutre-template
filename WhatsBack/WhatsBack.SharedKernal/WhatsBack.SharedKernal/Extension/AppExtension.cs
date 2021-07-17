using Microsoft.AspNetCore.Builder;

namespace WhatsBack.SharedKernal.Middlewares
{
    public static class AppExtension
    {
        public static void UseAppCustomMiddleware(this IApplicationBuilder app,string appName)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            UseSwaggerExtension(app,appName);
        }
        private static void UseSwaggerExtension( IApplicationBuilder app, string appName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appName}.WebApi");
            });
        }
    }
}
