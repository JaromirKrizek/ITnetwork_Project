using System.ComponentModel.DataAnnotations;  // Atributy u členských proměnných, např [Required]
using System.ComponentModel.DataAnnotations.Schema;  // DatabaseGenerated
using Microsoft.AspNetCore.Mvc.Rendering;  // SelectListItem - pro renderování v html

namespace Insurance_ASP.Models
{
    //#############################################################################################
    // Pojistná událost
    //#############################################################################################
    public class Incident
    {
        // Id bude primárním klíčem tabulky v databázi
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }

        // Slovní popis
        [Required(ErrorMessage = "Vyplňte popis události")]
        [Display(Name = "Popis události")]
        public string Description { get; set; }

        // Datum události
        [Required(ErrorMessage = "Vyplňte datum pojistné události")]
        [Display(Name = "Datum události")]
        [DataType(DataType.Date)]   // DateTime omezuje pouze na datum
        public DateTime? Date { get; set; }  // ? je použit proto aby fungovala ErrorMessage v atributu Required 

        // Stav události
        [Required(ErrorMessage = "Vyberte stav pojistné události")]
        [Display(Name = "Stav pojistné události")]
        public string Status { get; set; }

        // Výše plnění
        // [Required]  // Výši plnění na začátku nevyžadujeme, zadání není povinné, v db může být null
        // [RegularExpression(@"/^[+-]?([0-9]+\.?[0-9]*|\.[0-9]+)$/", ErrorMessage = "Vyplňte pojistnou částku")]
        [Range(0, double.MaxValue, ErrorMessage = "Cena nesmí být záporná")]
        [Display(Name = "Výše plnění")]
        public decimal? Amount { get; set; }  // ? je použit proto aby fungovala ErrorMessage v atributu Required 

        //--------------------------------------------------------------------------
        // Navigační vlastnosti - Vazba 1:N mezi entitami Insurance |- --<| Incident
        //--------------------------------------------------------------------------
        [ForeignKey("Insurance")]             // Definuje z jaké entity je FK
        public int InsuranceId { get; set; }  // Cizí klíč na pojištění

        // Díky slovu virtual tato vlastnost v db tabulce nebude.
        public virtual Insurance Insurance { get; set; }  // Odkaz na pojištění

        //-----------------------------------------------------------------------------------------
        // Vytvoří seznam stavů pojišťovací události pro rozevírací seznam ve view:
        //-----------------------------------------------------------------------------------------
        public static List<SelectListItem> GetStatusTypes()
        {
            var insuranceTypes = new List<SelectListItem>();
            insuranceTypes.Add(new SelectListItem { Text = "Oznámeno", Value = "Oznámeno", Selected = true });
            insuranceTypes.Add(new SelectListItem { Text = "Vyřizuje se", Value = "Vyřizuje se" });
            insuranceTypes.Add(new SelectListItem { Text = "Vyplaceno", Value = "Vyplaceno" });
            insuranceTypes.Add(new SelectListItem { Text = "Zamítnuto", Value = "Zamítnuto" });
            return insuranceTypes;
        }

        //-----------------------------------------------------------------------------------------

    }
}
