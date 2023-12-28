using Assesment.Models;

namespace Assesment.Services.CountryService
{
    public interface ICountryApiService
    {
        Task<List<Country>> GetCountriesFromApiAsync(string apiUrl);
    }
}