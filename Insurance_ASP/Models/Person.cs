﻿using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]
using System.ComponentModel.DataAnnotations.Schema;  // DatabaseGenerated

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Pojištěná osoba
    //#############################################################################################
    public class Person
    {
        // Id bude primárním klíčem tabulky v databázi
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vyplňte jméno")]  // Validace na serveru a not-null v db
        [Display(Name = "Jméno")]  // Popis vlastnosti, který se zobrazí ve view
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Vyplňte příjmení")]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Vyplňte email")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa")]   // Validace na serveru
        [DataType(DataType.EmailAddress)]                           // Validace na serveru
        [Display(Name = "Email")]
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

        //------------------------------------------------------------------------
        // Navigační vlastnosti - Vazba 1:N mezi entitami Person |- --<| Insurance
        //------------------------------------------------------------------------
        // Díky slovu virtual tato vlastnost v db tabulce nebude.
        // Usually virtual properties are being used by Entity Framework for the lazy loading of related objects.
        public virtual ICollection<Insurance> Insurances { get; set; }

        //----------------------------------------------------------------------------
        // Další nedatabázové (virtual) vlastnosti, které slouží k vytvožení uživatele
        //----------------------------------------------------------------------------
        [NotMapped]  // Tato vlastnost nebude v databázové tabulce
        [Required(ErrorMessage = "Zadejte heslo")]
        [StringLength(100, ErrorMessage = "Heslo musí být alespoň 8 znaků dlouhé", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public virtual string Password { get; set; }

        [NotMapped]  // Tato vlastnost nebude v databázové tabulce
        [Required(ErrorMessage = "Zadejte heslo")]
        [Compare("Password", ErrorMessage = "Zadaná hesla se neshodují")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrzení hesla")]
        public virtual string ConfirmPassword { get; set; }

    }
}
