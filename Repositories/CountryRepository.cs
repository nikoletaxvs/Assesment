using Assesment.Data;
using Assesment.Models;
using System.Diagnostics.Metrics;

namespace Assesment.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCountry(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }
            bool exists = CountryExistsInDb(country);
            if (exists)
            {
                return;
            }
            _context.Countries.Add(country);
            _context.SaveChanges();
        }

        public void GetCoutries()
        {
            throw new NotImplementedException();
        }

        public bool CountryExistsInDb(Country country)
        {
            return _context.Countries.Any(e => e.CommonName == country.CommonName);
        }
    }
}
