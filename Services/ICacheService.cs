namespace Reddis_NetCore6.Services
{
    public interface ICacheService
    {
        Task<string> GetValue(string key);
        Task<bool> SetValue(string key, string value);    
        Task<T> GetOrAdd <T>(string key, Func<Task<T>> action)where T:class;
        T GetOrAdd<T>(string key,Func<T> action)where T:class;
        Task Clear(string key);
        void ClearAll();
    }
}
