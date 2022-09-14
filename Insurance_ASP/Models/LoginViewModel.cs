using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Model pro přihlašovací formulář (Základy lekce 13, Eshop lekce 9)
    //#############################################################################################
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zadejte email")]                      // Validace na serveru
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]       // Validace na serveru
        [DataType(DataType.EmailAddress)]                               // Validace na serveru
        [Display(Name = "Email")]                // Popis vlastnosti, který se zobrazí ve view
        public string Email { get; set; }

        [Required(ErrorMessage = "Zadejte heslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; }

        [Display(Name = "Pamatuj si mě")]
        public bool RememberMe { get; set; }
    }
}
