using FluentValidation;
using Ioon.Application.Services;
using Ioon.Domain.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Ioon.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddDependecies();
            services.AddEmailService();
            return services;
        }
        private static IServiceCollection AddDependecies(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
            });

            services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

            services.AddSingleton<IHasherService, HasherService>();
            services.AddSingleton<IJwtService, JwtService>();
            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //});

            //services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders();

            return services;
        }
    }
}
