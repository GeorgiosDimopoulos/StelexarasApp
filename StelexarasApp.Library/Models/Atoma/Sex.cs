using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Models.Atoma
{
    public enum Sex        
    {
        [Display(Name = "Γυναίκα")] // 0
        Female,

        [Display(Name = "Άνδρας")] // 1
        Male
    }
}
