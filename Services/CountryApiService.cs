using Assesment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assesment.Services
{
    public class CountryApiService : ICountryApiService
    {
        private readonly HttpClient _httpClient;

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
            try
            {
                //var countries = JsonConvert.DeserializeObject<List<Country>>(response);
                var details = JArray.Parse(response);

                List<Country> result = new List<Country>();
                for (int i = 0; i < details.Count(); i++)
                {
                    Country country = new Country
                    {
                        CommonName = details[i]["name"]?["common"]?.ToString(),
                        Capital = details[i]["capital"]?[0]?.ToString()
                    };

                    var borders = details[i]["borders"]?.ToObject<List<string>>() ?? new List<string>();
                    for (int j = 0; j < borders.Count(); j++)
                    {
                        Border border = new Border
                        {
                            Name = borders[j].ToString()
                        };
                        country.Borders.Add(border);
                    }
                    result.Add(country);
                }
                return result;
            }
            catch (JsonReaderException ex)
            {
                throw;
            }

        }
    }
}
