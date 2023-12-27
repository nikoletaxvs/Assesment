using Assesment.DTOs;
using Assesment.Models;

namespace Assesment.Services.Cache
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        object RemoveData(string key);
        public void SetCountryDataToCache(List<Country> countries);
        public IEnumerable<CountryDto> GetFromCacheOrDatabase();
    }
}
