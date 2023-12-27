using Assesment.DTOs;
using Assesment.Models;

namespace Assesment.Repositories
{
    public interface ICountryRepository
    {
        public void AddCountry(Country country);
        public void AddCoutryList(List<Country> countries);
        public List<Country> GetCoutries();
        public bool DatabaseIsEmpty();
    }
}
