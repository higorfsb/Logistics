using Microsoft.AspNetCore.Mvc;

namespace Logistics.Application.Configurations
{
    public static class WebApiConfiguration
    {
        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(o =>
            {
                o.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();

            return app;
        }
    }
}
