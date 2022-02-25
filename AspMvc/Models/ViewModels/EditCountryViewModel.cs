using System.ComponentModel.DataAnnotations;

namespace AspMvc.Models.ViewModels
{

    public class EditCountryViewModel
    {
        [Required]
        public int CountryId { get; set; }
        
        [Required]
        [Display(Name="Country name")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        public bool IsValidForm()
        {
            return (!string.IsNullOrWhiteSpace(Name));
        }
    }

}