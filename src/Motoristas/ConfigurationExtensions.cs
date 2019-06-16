using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motoristas
{
    public static class ConfigurationExtensions
    {
        public static TOptions GetConfig<TOptions>(this IConfiguration config, string sectionName)
            where TOptions : class, new()
        {
            if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException(nameof(sectionName));
            var options = new TOptions();
            config.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
