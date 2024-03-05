using Ioon.Application.Common.Interfaces.Data;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Common.Interfaces.Repositories;
using Ioon.Infrastructure.Context;
using Ioon.Infrastructure.Data;
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
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlProvider")));

            services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        private static IServiceCollection AddDependecies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

    }
}
