using Microsoft.Extensions.Configuration;

namespace UrlShortner.Services
{
    public class ConfigService : IConfigService
    {
        private IConfigurationRoot _config;

        public ConfigService(IConfigurationRoot config)
        {
            _config = config;
        }

        public string GetConfiguration(string key)
        {
            return _config[key];
        }
    }
}