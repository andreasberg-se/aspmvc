using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspMvc.Models
{

    public class City
    {
        [Key]
        public int CityId { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Person> People { get; set; }
    }

}