using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApi.Extensions
{
    public static class AuthenticationServicesExtensions
    {
        /// <summary>
        /// add authentication services to application service pool
        /// </summary>
        /// <param name="services">services collection</param>
        /// <param name="configuration">configuration from appsettings.json</param>
        /// <returns>services collection includes the authentication service</returns>
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            {
                // Configure JWT authentication
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["Jwt:Issuer"],
                            ValidAudience = configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:TokenKey"]))
                        };
                    });

                return services;
            }
        }
    }
}
