using System.ComponentModel.DataAnnotations;

namespace AspMvc.Models.ViewModels
{

    public class CreatePersonViewModel
    {
        [Display(Name="First name")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Display(Name="Last name")]
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Display(Name="City")]
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string City { get; set; }

        [Display(Name="Phone")]
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Phone { get; set; }

        public bool IsValidForm()
        {
            return ((!string.IsNullOrWhiteSpace(FirstName))
            && (!string.IsNullOrWhiteSpace(LastName))
            && (!string.IsNullOrWhiteSpace(City))
            && (!string.IsNullOrWhiteSpace(Phone)));
        }

        public CreatePersonViewModel() {}

        public CreatePersonViewModel(string firstName, string lastName, string city, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Phone = phone;
        }
    }

}
