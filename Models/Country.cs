namespace Assesment.Models
{
    
    public class Country
    {
        public class Name
        {
            public string? common { get; set; }
           
        }

        
        public Name? name { get; set; }

        public List<string> capital { get; set; }
        public List<string>? borders { get; set; }

    }
}
