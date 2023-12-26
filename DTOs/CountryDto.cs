using Assesment.Models;

namespace Assesment.DTOs
{
    public class CountryDto
    {
        
        public string CommonName{ get; set; }
        public string Capital {  get; set; }    
        public IEnumerable<BorderDto>? Borders { get; set; }
    }
    public class BorderDto
    {
        public string Name { get; set; }
    }
}
