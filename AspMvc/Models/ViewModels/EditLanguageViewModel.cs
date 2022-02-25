using System.ComponentModel.DataAnnotations;

namespace AspMvc.Models.ViewModels
{

    public class EditLanguageViewModel
    {
        [Required]
        public int LanguageId { get; set; }

        [Required]
        [Display(Name="Language")]
        [StringLength(100, MinimumLength = 1)]
        public string LanguageName { get; set; }

        public bool IsValidForm()
        {
            return (!string.IsNullOrWhiteSpace(LanguageName));
        }
    }

}