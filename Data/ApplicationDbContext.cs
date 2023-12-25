using Assesment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assesment.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
        public DbSet<Country> Countries {  get; set; }
    }
}
