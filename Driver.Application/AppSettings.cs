using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Driver.Application
{
    public class AppSettings
    {
        private const string AppSettingsKey = "AppSettings";

        private readonly static IConfigurationRoot configuration;
        public static readonly IConfigurationSection AppSettingsSection;

        public static readonly int CommandTimeout;
        public static readonly string BindingUrl;
        public static readonly string GoogleApiKey;
        public static readonly string BasePath;

        static AppSettings()
        {
            BasePath = Directory.GetCurrentDirectory();

            configuration = new ConfigurationBuilder()
                .SetBasePath(BasePath)
                .AddJsonFile("appsettings.json")
                .Build();

            AppSettingsSection = GetSection(AppSettingsKey);

            CommandTimeout = Convert.ToInt32(AppSettingsSection[nameof(CommandTimeout)]);
            BindingUrl = AppSettingsSection[nameof(BindingUrl)];
            GoogleApiKey = AppSettingsSection[nameof(GoogleApiKey)];
        }

        public static IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }
    }
}
