using API.Middleware;
using Ioon.Application.Loggin;
using Ioon.Application.Services;
using Ioon.Domain.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configura la autenticación JWT en la aplicación ASP.NET Core.
        /// </summary>
        /// <param name="Services">La colección de servicios de la aplicación.</param>
        /// <param name="Configuration">La configuración de la aplicación, incluyendo la configuración del token JWT.</param>
        /// <returns>La colección de servicios actualizada.</returns>
        public static IServiceCollection AddAuthJWT(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["JWT:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = Configuration["JWT:Audience"],

                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]!))
                    };
                });

            Services.AddAuthorization();

            return Services;
        }

        /// <summary>
        /// Configura la política CORS (Compartir Recursos de Origen Cruzado) basándose en los orígenes especificados.
        /// </summary>
        /// <param name="services">La <see cref="IServiceCollection"/> a la que se agregarán los servicios CORS.</param>
        /// <param name="configuration">La <see cref="IConfiguration"/> que contiene la configuración CORS.</param>
        public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                // Obtén los orígenes permitidos desde la configuración
                var origins = configuration.GetSection("Security:AllowCorsOrigins").Get<string[]>()!;

                // Agrega una política CORS predeterminada con los orígenes especificados, permitiendo cualquier método y encabezado
                options.AddDefaultPolicy(builder =>
                    builder.WithOrigins(origins)
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Agrega un middleware personalizado a la canalización de la aplicación.
        /// </summary>
        /// <param name="application">La <see cref="IApplicationBuilder"/> a la que se agrega el middleware.</param>
        /// <returns>La <see cref="IApplicationBuilder"/> modificada con el middleware agregado.</returns>
        public static IApplicationBuilder AddCustomMiddleware(this IApplicationBuilder application)
        {
            // Usa el middleware personalizado llamado "IoonMiddleware"
            return application.UseMiddleware<IoonMiddleware>();
        }


        private static ILoggingBuilder ConfigCustomLogger(this ILoggingBuilder builder, Action<IoonLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, IoonLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }

        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder, IConfiguration configuration)
        {          
            builder.ConfigCustomLogger((provider) =>
            {
                var logFields = configuration.GetSection("Logging");

                if (logFields.Exists())
                {
                    logFields.Bind(provider);
                  
                }          
            });
            return builder;
        }
    }
}
