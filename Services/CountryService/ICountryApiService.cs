using Assesment.Models;

namespace Assesment.Services.CountryService
{
    public interface ICountryApiService
    {
        Task<List<Country>> GetCountriesAsync(string apiUrl);
    }
}