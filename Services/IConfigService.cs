namespace UrlShortner.Services
{
    public interface IConfigService
    {
        string GetConfiguration(string key);
    }
}