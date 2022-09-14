using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]
using System.ComponentModel.DataAnnotations.Schema;  // DatabaseGenerated
using Microsoft.AspNetCore.Mvc.Rendering;  // SelectListItem - pro renderování v html

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Pojištění
    //#############################################################################################
    public class Insurance
    {
        // Id bude primárním klíčem tabulky v databázi
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        // Typ pojištění:
        [Required(ErrorMessage = "Vyplňte typ pojištění")]
        [Display(Name = "Typ pojištění")]
        public string Type { get; set; }

        // Pojistná částka:
        [Required(ErrorMessage = "Vyplňte pojistnou částku")]
        //[RegularExpression(@"/^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$/", ErrorMessage = "Vyplňte pojistnou částku")]
        [Range(0, double.MaxValue, ErrorMessage = "Cena nesmí být záporná")]
        [Display(Name = "Pojistná částka")]
        public decimal? Amount { get; set; }  // ? je použit proto aby fungovala ErrorMessage v atributu Required 

        // Předmět pojištění:
        [Required(ErrorMessage = "Vyplňte předmět pojištění")]
        [Display(Name = "Předmět pojištění")]
        public string Subject { get; set; }

        // Platnost od:
        [Required(ErrorMessage = "Vyplňte začátek platnosti pojištění")]
        [Display(Name = "Platnost od")]
        [DataType(DataType.Date)]   // DateTime omezuje pouze na datum
        public DateTime? ValidFrom { get; set; }  // ? je použit proto aby fungovala ErrorMessage v atributu Required 

        // Platnost do:
        [Required(ErrorMessage = "Vyplňte konec platnosti pojištění")]
        [Display(Name = "Platnost do")]
        [DataType(DataType.Date)]
        public DateTime? ValidTo { get; set; }  // ? je použit proto aby fungovala ErrorMessage v atributu Required 

        //------------------------------------------------------------------------
        // Navigační vlastnosti - Vazba 1:N mezi entitami Person |- --<| Insurance
        //------------------------------------------------------------------------
        [ForeignKey("Person")]             // Definuje z jaké entity je FK
        public int PersonId { get; set; }  // Cizí klíč na pojištěného

        // Díky slovu virtual tato vlastnost v db tabulce nebude.
        public virtual Person Person { get; set; }  // Odkaz na pojištěného

        //--------------------------------------------------------------------------
        // Navigační vlastnosti - Vazba 1:N mezi entitami Insurance |- --<| Incident
        //--------------------------------------------------------------------------
        // Díky slovu virtual tato vlastnost v db tabulce nebude.
        public virtual ICollection<Incident> Incidents { get; set; }

        //-----------------------------------------------------------------------------------------
        // Vytvoří seznam typů pojištění pro rozevírací seznam ve view:
        //-----------------------------------------------------------------------------------------
        public static List<SelectListItem> GetInsuranceTypes()
        {
            var insuranceTypes = new List<SelectListItem>();
            insuranceTypes.Add(new SelectListItem { Text = "Pojištění osob", Value = "Pojištění osob", Selected = true });
            insuranceTypes.Add(new SelectListItem { Text = "Pojištění firem", Value = "Pojištění firem" });
            insuranceTypes.Add(new SelectListItem { Text = "Pojištění majetku", Value = "Pojištění majetku" });
            insuranceTypes.Add(new SelectListItem { Text = "Pojištění zájmů", Value = "Pojištění zájmů" });
            return insuranceTypes;
        }

        //-----------------------------------------------------------------------------------------

    }
}
