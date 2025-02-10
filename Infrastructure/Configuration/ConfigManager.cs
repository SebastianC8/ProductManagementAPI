using Microsoft.Extensions.Configuration;

namespace Infrastructure.Configuration
{
    public class ConfigManager
    {
        public static IConfiguration AppSetting { get; }

        static ConfigManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
