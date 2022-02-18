using System.ComponentModel.DataAnnotations;

namespace AspMvc.Models.ViewModels
{

    public class CityViewModel
    {
        [Required]
        [Display(Name="City name")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name="Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public bool IsValidForm()
        {
            return (!string.IsNullOrWhiteSpace(Name));
        }
    }

}