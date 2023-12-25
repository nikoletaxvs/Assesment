namespace Assesment.DTOs
{
    public class CountryDto
    {
        
        public string CommonName{ get; set; }
        public IEnumerable<string> Capital {  get; set; }    
        public IEnumerable<string>? Borders { get; set; }
    }
}
