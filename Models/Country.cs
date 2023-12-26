using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assesment.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
    
        public string? CommonName { get; set; }
        public string? Capital { get; set; }
        public List<Border>? Borders { get; set; } = new List<Border>();

    }
    public class Border
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public int CountryId {  get; set; }
        //public Country Country { get; set; } 
    }

}
