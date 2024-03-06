using Logistics.Domain.Builders.User;
using Logistics.Domain.Interfaces.Builders;
using Logistics.Domain.Interfaces.Repositories;
using Logistics.Domain.Interfaces.Services;
using Logistics.Domain.Services;
using Logistics.Insfrastructure.Repositores;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Logistics.Application.Configurations
{
    public static class DependencyConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            // Work Interfaces
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            //Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOccurrenceRepository, OccurrenceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Services
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOccurrenceService, OccurrenceService>();
            services.AddScoped<IAuthService, AuthService>();

            // Builders
            services.AddScoped<ILoginResponseBuilder, LoginResponseBuilder>();

            return services;
        }
    }
}
