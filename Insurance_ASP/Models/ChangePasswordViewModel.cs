using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Model pro formulář se změnou hesla (Eshop lekce 9)
    //#############################################################################################
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Aktuální heslo je povinné")]               // Validace na serveru
        [DataType(DataType.Password)]                                        // Validace na serveru
        [Display(Name = "Aktuální heslo")]            // Popis vlastnosti, který se zobrazí ve view   
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nové heslo je povinné")]
        [StringLength(100, ErrorMessage = "Heslo musí být alespoň 8 znaků dlouhé", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Nové heslo")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Zadaná hesla se neshodují")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení nového hesla")]
        public string ConfirmPassword { get; set; }
    }
}
