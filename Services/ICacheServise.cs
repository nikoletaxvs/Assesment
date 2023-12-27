namespace Assesment.Services
{
    public interface ICacheServise
    {
        T GetData<T>(string key);
        bool SetData<T>(string key,T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
    }
}
