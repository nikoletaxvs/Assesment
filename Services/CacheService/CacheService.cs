using Assesment.DTOs;
using Assesment.Models;
using Assesment.Repositories;
using AutoMapper;
using StackExchange.Redis;
using System.Text.Json;

namespace Assesment.Services.Cache
{
    public class CacheService : ICacheService
    {
        IDatabase _cacheDb;
        IMapper _mapper;
        ICountryRepository _countryRepository;
        private readonly string redisPort = "localhost:6379";
        public CacheService(IMapper mapper,
            ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
            var redis = ConnectionMultiplexer.Connect(redisPort);

            _cacheDb = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);
            if (_exist)
            {
                return _cacheDb.KeyDelete(key);
            }
            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expirtyTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirtyTime);

        }
        public void SetCountryDataToCache(List<Country> countries)
        {
            //Add data to cache and set an expiry time
            var expiryTime = DateTimeOffset.Now.AddSeconds(15);
            SetData<IEnumerable<CountryDto>>("countries", countries.Select(coutryDto => _mapper.Map<CountryDto>(coutryDto)), expiryTime);
        }
        public IEnumerable<CountryDto> GetFromCacheOrDatabase()
        {
            var cacheData = GetData<IEnumerable<CountryDto>>("countries");
            if (cacheData != null && cacheData.Count() > 0)
            {
                return cacheData;
            }
            bool countriesExistInDatabase = _countryRepository.DatabaseIsNotEmpty();
            //check if database has any data
            if (countriesExistInDatabase)
            {
                return _countryRepository.GetCoutries().Select(coutryDto => _mapper.Map<CountryDto>(coutryDto));
            }
            return default;
        }
    }
}
