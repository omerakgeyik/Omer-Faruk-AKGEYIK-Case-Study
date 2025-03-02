using Microsoft.Extensions.Configuration;

namespace CaseStudy.Utilities
{
    public class ConfigReader
    {
        private static IConfigurationRoot configuration;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        public static string GetProperty(string key)
        {
            return configuration[key];
        }
    }
}
