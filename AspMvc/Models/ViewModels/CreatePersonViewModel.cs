using System.ComponentModel.DataAnnotations;

namespace AspMvc.Models.ViewModels
{

    public class CreatePersonViewModel
    {
        [Required]
        [Display(Name="First name")]
        [StringLength(100, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name="Last name")]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; }

        [Display(Name="City")]
        public int CityId { get; set; }

        [Display(Name="Phone")]
        [StringLength(20)]
        public string Phone { get; set; }

        public bool IsValidForm()
        {
            return ( (!string.IsNullOrWhiteSpace(FirstName))
                && (!string.IsNullOrWhiteSpace(LastName))
                && (CityId != 0)
                && (!string.IsNullOrWhiteSpace(Phone)) );
        }

        public CreatePersonViewModel() {}

        public CreatePersonViewModel(string firstName, string lastName, int cityId, string phone) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            CityId = cityId;
            Phone = phone;
        }
    }

}
