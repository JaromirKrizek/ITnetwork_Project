using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Model pro registrační formulář (Základy lekce 14, Eshop lekce 9)
    //#############################################################################################
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vyplňte jméno")]  // Validace na serveru a not-null v db
        [Display(Name = "Jméno")]  // Popis vlastnosti, který se zobrazí ve view
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vyplňte příjmení")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vyplňte email")]                         // Validace na serveru
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]          // Validace na serveru
        [DataType(DataType.EmailAddress)]                                  // Validace na serveru
        [Display(Name = "Email")]                   // Popis vlastnosti, který se zobrazí ve view
        public string Email { get; set; }

        [Required(ErrorMessage = "Vyplňte telefon")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        // [Required]  // Ulici nevalidujeme, její zadání není povinné, v db může být null
        [Display(Name = "Ulice")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Vyplňte číslo popisné")]
        [Display(Name = "Číslo popisné")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Vyplňte město")]
        [Display(Name = "Město")]
        public string City { get; set; }

        [Required(ErrorMessage = "Vyplňte PSČ")]
        [Display(Name = "PSČ")]
        public string PostCode { get; set; }

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
