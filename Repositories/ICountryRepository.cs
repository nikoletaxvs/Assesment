using Assesment.Models;

namespace Assesment.Repositories
{
    public interface ICountryRepository
    {
        public void AddCountry(Country country);
        public void GetCoutries();
    }
}
