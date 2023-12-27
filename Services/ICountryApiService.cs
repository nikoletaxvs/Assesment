using Assesment.Models;

namespace Assesment.Services
{
    public interface ICountryApiService
    {
        Task<List<Country>> GetCountriesAsync(string apiUrl);
    }
}