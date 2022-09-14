using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Insurance_ASP.Models;

namespace Insurance_ASP.Data
{
    //#############################################################################################
    public class ApplicationDbContext : IdentityDbContext
    {
        // Generování databázových tabulek na základě tříd v modelu:
        // Vlastnosti typu DbSet zajistí, že Entity Framework bude považovat naši třídu Person
        // za datový model a při migraci vytvoří odpovídající databázovou tabulku.
        // Tabulku pojmenuje podle názvu třídy, tj. Person atd.
        // Tento řádek se vygeneruje automaticky poté co vytvoříme PersonsController.cs 
        public DbSet<Insurance_ASP.Models.Person> Person { get; set; }
        public DbSet<Insurance_ASP.Models.Insurance> Insurance { get; set; }
        public DbSet<Insurance_ASP.Models.Incident> Incident { get; set; }

        //-----------------------------------------------------------------------------------------
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //-----------------------------------------------------------------------------------------
        // Přepíšeme tuto metodu pro manuální nastavení některých věcí
        //-----------------------------------------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Atributu Amount v tabulce Insurance, který je typu decimal nastavíme přesnost:
            builder.Entity<Insurance>().Property(x => x.Amount).HasColumnType("decimal(10,2)");
            builder.Entity<Incident>().Property(x => x.Amount).HasColumnType("decimal(10,2)");
        }

        //-----------------------------------------------------------------------------------------

    }

}