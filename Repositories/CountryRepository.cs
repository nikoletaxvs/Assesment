using Assesment.Data;
using Assesment.DTOs;
using Assesment.Models;
using Microsoft.EntityFrameworkCore;
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
        public IQueryable<Country> GetCoutries() => _context.Countries.Include(country => country.Borders);
        

        public bool CountryExistsInDb(Country country) => _context.Countries.Any(e => e.CommonName == country.CommonName);
        

        public bool DatabaseIsNotEmpty() => _context.Countries.Any();

        public bool DeleteAll()
        {
            var allCountries = _context.Countries.ToList(); // Load all countries into memory
            _context.Countries.RemoveRange(allCountries);
            return _context.SaveChanges() == 1;
        }
    }
}
