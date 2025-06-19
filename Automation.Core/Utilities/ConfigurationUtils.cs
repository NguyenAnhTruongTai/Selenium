using Microsoft.Extensions.Configuration;

namespace Automation.Core.Utilities
{
    public class ConfigurationUtils
    {
        private static IConfiguration _config;

        public static IConfiguration ReadConfiguration(string path)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path)
                .Build();
            _config = config;
            return config;
        }

        public static string GetConfigurationByKey(string key, IConfiguration? config = null)
        {
            var value = config == null ? _config[key] : config[key];

            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            throw new Exception($"Configuration key '{key}' not found or value is empty.");
        }

        public static string GetSectionValue(string section, string key)
        {
            return _config.GetSection(section)[key];
        }
    }
}