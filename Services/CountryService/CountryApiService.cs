using Assesment.Models;
using Assesment.Repositories;
using Assesment.Services.Cache;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Assesment.Services.CountryService
{
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ICountryRepository _countryRepository;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public CountryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Country>> GetCountriesAsync(string apiUrl)
        {
            //var apiUrl = "https://restcountries.com/v3.1/all";
            var response = await _httpClient.GetStringAsync(apiUrl);
            return ParseCountries(response);
        }

        private List<Country> ParseCountries(string response)
        {

            //var countries = JsonConvert.DeserializeObject<List<Country>>(response);
            var details = JArray.Parse(response);

            List<Country> result = new List<Country>();

            foreach (var detail in details)
            {
                Country country = new Country
                {
                    CommonName = detail["name"]?["common"]?.ToString(),
                    Capital = detail["capital"]?[0]?.ToString()
                };

                var borders = detail["borders"]?.ToObject<List<string>>() ?? new List<string>();
                foreach (var borderName in borders)
                {
                    country.Borders.Add(new Border { Name = borderName });
                }

                result.Add(country);
            }

            return result;


        }
    }
}
