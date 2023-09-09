namespace Shop.Domain.Infra.Caching
{
    public interface ICachingService
    {
        void Set(string key, string value);
        string Get(string key);
    }
}
