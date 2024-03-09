using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Common.Interfaces.Repositories;
using Ioon.Infrastructure.Persistence;
using Ioon.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ioon.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddDependecies();
            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions<DatabaseOptions>()
                .Configure((options) =>
                {
                    var dbSection = Configuration.GetSection("DBType");

                    if (dbSection.Exists())
                    {
                        dbSection.Bind(options);
                    }

                    options.ConnectionString = Configuration.GetConnectionString("SqlProvider")
                        ?? throw new InvalidOperationException("La cadena de conexión SqlProvider está ausente en appsettings.json.");
                });

            services.AddScoped<IDatabaseContext, DatabaseContext>();

            return services;

        
        }

        private static IServiceCollection AddDependecies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUserRepository<,>), typeof(AccountRepository));
            services.AddScoped(typeof(IProductRepository<,>), typeof(ProductRepository));

            return services;
        }

    }
}
