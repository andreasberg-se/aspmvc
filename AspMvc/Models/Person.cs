using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspMvc.Models
{

    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Phone { get; set; }

        public List<PersonLanguage> PersonLanguages { get; set; }
    }

}
