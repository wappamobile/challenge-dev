using Wappa.Middleware.Domain.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Wappa.Middleware.Core.Extensions
{
    public static class GoogleMapsExtentions
    {
        public static void AddGoogleMaps(this IServiceCollection services, Action<GoogleMapsConfiguration> configureOptions)
        {
            var options = new GoogleMapsConfiguration();
            configureOptions(options);
            services.Configure(configureOptions);
        }
    }
}
