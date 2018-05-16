using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RegisterServicesExtensions {
        public static void AddServices (this IServiceCollection services, Assembly assembly) {

            var types = assembly.GetTypes ();

            types.Where (t => !t.IsAbstract &&
                    t.Name.EndsWith ("Service", StringComparison.InvariantCultureIgnoreCase) &&
                    t.GetInterfaces ().Length == 1 &&
                    t.GetInterfaces () [0].Name.EndsWith ("Service", StringComparison.InvariantCultureIgnoreCase))
                .ToList ()
                .ForEach (t => {
                    services.AddSingleton (t.GetInterfaces () [0], t);
                });
        }
    }
}