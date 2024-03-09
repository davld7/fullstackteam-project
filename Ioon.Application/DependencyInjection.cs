using FluentValidation;
using Ioon.Application.Services;
using Ioon.Application.Services.Email;
using Ioon.Domain.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ioon.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependecies();
            services.AddEmailService(configuration);
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

        private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<EmailConfig>()
                .Configure((options) =>
                {
                    var jsonSection = configuration.GetSection("Email");

                    if (!jsonSection.Exists())
                    {
                        throw new Exception("Error al cargar la configuración del correo electrónico.");
                    }

                    jsonSection.Bind(options);
                });

            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
