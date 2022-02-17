using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspMvc.Models
{

    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public List<string> ProgrammingLanguages { get; set; }
        [NotMapped]
        public List<string> WebDevelopmentTools { get; set; }
        [NotMapped]
        public List<string> Databases { get; set; }
        [NotMapped]
        public List<Project> Projects { get; set; }

        public Person() {}

        public Person(int personId, string firstName, string lastName)
        {
            this.PersonId = personId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Person(int personId, string firstName, string lastName, string address, int zipCode, string city, string country, string phone, string email)
            : this(personId, firstName, lastName)
        {
            this.Address = address;
            this.ZipCode = zipCode;
            this.City = city;
            this.Country = country;
            this.Phone = phone;
            this.Email = email;
        }
    }

}
