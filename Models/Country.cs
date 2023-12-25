using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assesment.Models
{

  
    //public class Country
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    [Required]
    //    public NameInfo Name { get; set; }
    //    [Required]
    //    public List<string> Capital { get; set; }
    //    public List<string> Borders { get; set; }

    //}

    //public class NameInfo
    //{
    //    public string Common { get; set; }

    //}
    public class Country
    {
        public string Id { get; set; }
        public string CommonName { get; set; }
        public string Capital { get; set; }
        public List<string>? Borders { get; set; }

    }
    public class Border
    {
        public string Id { get; set; }
        public string Name{ get; set; }

        public Country Country { get; set; } // Navigation property
    }

}
