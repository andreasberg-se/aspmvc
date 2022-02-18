using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AspMvc.Models
{

    public class PersonLanguage
    {
        [Display(Name="Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        [Display(Name="Language")]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }

}