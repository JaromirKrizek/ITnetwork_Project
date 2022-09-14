using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Model pro registrační formulář (Základy lekce 14, Eshop lekce 9)
    //#############################################################################################
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Zadejte email")]                         // Validace na serveru
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]          // Validace na serveru
        [DataType(DataType.EmailAddress)]                                  // Validace na serveru
        [Display(Name = "Email")]                   // Popis vlastnosti, který se zobrazí ve view
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [StringLength(100, ErrorMessage = "Heslo musí být alespoň 8 znaků dlouhé", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [Compare("Password", ErrorMessage = "Zadaná hesla se neshodují")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        public string ConfirmPassword { get; set; }
    }
}
