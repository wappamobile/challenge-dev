using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace Wappa.Middleware.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection GetGoogleMapsConfigurations(this IConfiguration configuration)
        {
            return configuration.GetSection("GetGoogleMapsConfigurations");
            
        }

        public static string GetDefaultPolicy(this IConfiguration configuration)
        {
            string result = configuration.GetValue<string>("Policies:Default");
            return result;
        }

        public static string[] GetCorsOrigins(this IConfiguration configuration)
        {
            string[] result =
                configuration.GetValue<string>("App:CorsOrigins")
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            return result;
        }

        public static string GetIssuerSigningKey(this IConfiguration configuration)
        {
            string result = configuration.GetValue<string>("Authentication:JwtBearer:SecurityKey");
            return result;
        }

        public static string GetValidIssuer(this IConfiguration configuration)
        {
            string result = configuration.GetValue<string>("Authentication:JwtBearer:Issuer");
            return result;
        }

        public static string GetValidAudience(this IConfiguration configuration)
        {
            string result = configuration.GetValue<string>("Authentication:JwtBearer:Audience");
            return result;
        }

        public static IConfigurationSection GetBusConfiguration(this IConfiguration configuration)
        {
            return configuration.GetSection("BusConfigurations");

        }

        public static IConfigurationSection GetRabbitMQConfigurations(this IConfiguration configuration)
        {
            return configuration.GetSection("RabbitMQConfigurations");

        }

        public static IConfigurationSection GetWppConfigurations(this IConfiguration configuration)
        {
            return configuration.GetSection("WPPConfigurations");

        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(this IConfiguration configuration)
        {
            var issuerSigningKey = configuration.GetIssuerSigningKey();
            var data = Encoding.UTF8.GetBytes(issuerSigningKey);
            var result = new SymmetricSecurityKey(data);
            return result;
        }

    }
}
