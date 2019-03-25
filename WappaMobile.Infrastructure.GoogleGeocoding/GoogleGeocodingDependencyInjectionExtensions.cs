using Microsoft.Extensions.DependencyInjection;
using WappaMobile.Application.Services.Geocoding;
using WappaMobile.Domain;
using WappaMobile.Infrastructure.GoogleGeocoding;

namespace WappaMobile.Infrastructure
{
    public static class GoogleGeocodingDependencyInjectionExtensions
    {
        public static IServiceCollection AddGoogleGeocoder(this IServiceCollection services, string apiKey)
        {
            services.AddTransient<IGeocoder, GoogleGeocoder>(sp => new GoogleGeocoder(apiKey));

            return services;
        }
    }
}
