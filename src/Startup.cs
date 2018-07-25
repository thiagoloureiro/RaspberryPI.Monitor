using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nancy.Owin;

namespace MonitorARM
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(option =>
            {
                option.AllowAnyOrigin();
                option.AllowAnyHeader();
                option.AllowAnyMethod();
            });

            app.UseOwin(x => x.UseNancy());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
        }
    }
}