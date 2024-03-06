using Logistics.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Application.Configurations
{
    public static class ContextConfig
    {
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LogisticsContext>(options => options
           .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
