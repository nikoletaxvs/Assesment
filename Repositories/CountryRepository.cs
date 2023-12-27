using Assesment.Data;
using Assesment.DTOs;
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
        public void AddCoutryList(List<Country> countries)
        {
            foreach (var country in countries)
            {
                AddCountry(country);
               
            }
        }
        public List<Country> GetCoutries()
        {
            return _context.Countries.ToList();
        }

        public bool CountryExistsInDb(Country country)
        {
            return _context.Countries.Any(e => e.CommonName == country.CommonName);
        }

        public bool DatabaseIsEmpty()
        {
            return _context.Countries.Any();
        }
    }
}
