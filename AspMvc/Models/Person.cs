using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string Address { get; set; }
        public int ZipCode { get; set; }
        [Required]
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<string> ProgrammingLanguages { get; set; }
        public List<string> WebDevelopmentTools { get; set; }
        public List<string> Databases { get; set; }
        public List<Project> Projects { get; set; }

        public Person() {}

        public Person(int personId, string firstName, string lastName)
        {
            this.PersonId = personId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Person(int personId, string firstName, string lastName, string address, int zipCode, string city, string country, string phone, string email) : this(personId, firstName, lastName)
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
