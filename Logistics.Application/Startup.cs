using Logistics.Application.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Logistics.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerConfig();
            services.ResolveDependencies();
            services.AddWebApiConfiguration();
            services.AddDatabaseContext(Configuration);
            services.AddJwtConfiguration(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseExceptionHandler("/error");
            app.UseWebApiConfiguration(env);
            app.UseSecurityConfiguration();
            app.UseSwaggerConfig(env, provider, "");
        }
    }
}
