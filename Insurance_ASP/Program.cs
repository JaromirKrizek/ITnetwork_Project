/*#################################################################################################
Insurance_ASP - Závìreèný projekt
###################################################################################################

- Založení projektu
   - Podle šablony ASP.NET Core Web App (Model-View-Controller)
   - Verzi frameworku vybereme .NET 6.0.
   - U výbìru typu ovìøení nastavíme Individual Accounts
   - Zaškrtneme "Do not use top-level statements"

- Nainstalování nugetu Microsoft.EntityFrameworkCore.Proxies
   - Jak se to dìlá je popsáno v této lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni
   - Nuget je potøeba k tomu, aby probíhal automatický lazy loading navigaèních vlastností v
     modelech sloužících k vazbám mezi entitami.

- Použití .UseLazyLoadin­gProxies() 
   - V souboru program.cs, jak je popsáno v lekci 5:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-migrace-a-prvni-spusteni
   - Je potøeba ze stejných dùvodù jako nuget Microsoft.EntityFrameworkCore.Proxies výše.

- Vypnutí nullovatelných referenèních typù
   - Vypneme smazáním øádku <Nullable>enable</Nullable> v elementu <PropertyGroup>, který 
     nalezneme v aktuálnì otevøeném .csproj (otevøeme poklepáním na hlavní složku projektu
     v solution exploreru)
   - Jak se to dìlá je popsáno v této lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni

- Do složky wwwroot pøidáme novou složku "images", do které budeme ukládat pøípadné obrázky.

- Vytvoøení datového modelu "Person"
   - Do složky Models pøidáme novou tøídu "Person".

- Vytvoøení controlleru "PersonsController.cs"
   - pravým na složku "Controllers" -> Add -> Controller -> 
     -> MVC Controller with views, using Entity Framework -> Model class "Person" ->
     -> Data context class "ApplicationDbContext" -> odšktrnout "Reference script libraries" -> Add
   - Tím se v ApplicationDbContext.cs pøidá øádek:
     public DbSet<Insurance_ASP.Models.Person>? Person { get; set; }
     Ten definuje, že se ze tøídy "Person" pøi migraci databáze vytvoøí databázová tabulka.

- Provedena migrace databáze (Pøidání tabulky Person)
   - Rebuild
   - Roleta Tools -> NuGet Package Manager -> Package Manager Console -> Do konzole napsat:
     Add-Migration Create_Table_Person
     Update-Database
   - Databáze je umístìna v adresáøi C:\Users\Admin\ jako soubor .mdf

- Vytvoøení datového modelu "Insurance"
   - Do složky Models pøidáme novou tøídu "Insurance".

- Úprava tøídy ApplicationDbContext:
   - Pøesat metodu OnModelCreating a v ní atributu Amount v tabulce Insurance, 
     který je typu decimal nastavíme pøesnost.

- Úprava tøídy Person
   - Kvùli vazbì 1: N mezi entitami Person |- --<| Insurance nutno pøidat vlastnost:
     public virtual ICollection<Insurance> Insurances

- Vytvoøení controlleru "InsurancesController.cs"
   - Analogicky jako u "PersonsController.cs"

- Provedena migrace databáze (Pøidání tabulky Insurance a update tabulky Person)
   - Rebuild
   - Add-Migration Create_Table_Insurance
   - Update-Database
   - Naše databáze nyní vypadá nìjak takto:

     -----------------------            -----------------------------
     | Person              |            | Insurance                 |
     |---------------------|            |---------------------------|
     | #* Id (PK not-null) |            | #* Id (PK not-null)       |
     |    Další parametry  |- - - -----<|  * PersonId (FK not-null) |
     |                     |            |    Další parametry        |
     |                     |            |                           |
     -----------------------            -----------------------------

- Stylování pojištìncù
   - Index - seznam všech pojištìncù - OK
   - Create - Nový pojištìnec - OK
   - Detail - Údaje o pojištìnci vèetnì sjednaných pojištìní - OK
   - Edit - Editace pojištìnce - OK
   - Delete - Smazání pojištìnce - OK

- Stylování pojištìní
   - Index - seznam všech pojištìní - OK
   - Create - Nové pojištìní - OK
   - Detail - Údaje o pojištìní - OK
   - Edit - Editace pojištìní - OK
   - Delete - Smazání pojištìní - OK

- Datové úpravy pojištìní:
   - Datum a èas nahradit pouze datumem - OK
     Vyøešeno použitím atributu [DataType(DataType.Date)]

- Stylování navigace, zápatí a hlavní stránky

- Vytvoøení datového modelu "Incident" na pojistné události
   - Do složky Models pøidáme novou tøídu "Incident".

- Úprava tøídy ApplicationDbContext:
   - Do metody OnModelCreating a do ní pøidat Amount v tabulce Incident, 
     který je typu decimal nastavíme pøesnost.

- Úprava tøídy Insurance
   - Kvùli vazbì 1: N mezi entitami Insurance |- --<| Incident nutno pøidat vlastnost:
     public virtual ICollection<Incident> Incidents

- Vytvoøení controlleru "IncidentsController.cs"
   - Analogicky jako u "PersonsController.cs"

- Provedena migrace databáze (Pøidání tabulky Incident a update tabulky Person)
   - Rebuild
   - Add-Migration Create_Table_Insurance
   - Update-Database
   - Naše databáze nyní vypadá nìjak takto:

     -----------------------            -----------------------------            --------------------------------
     | Person              |            | Insurance                 |            | Incident                     |
     |---------------------|            |---------------------------|            |------------------------------|
     | #* Id (PK not-null) |            | #* Id (PK not-null)       |            | #* Id (PK not-null)          |
     |    Další parametry  |- - - -----<|  * PersonId (FK not-null) |- - - -----<|  * InsuranceId (FK not-null) |
     |                     |            |    Další parametry        |            |    Další parametry           |
     |                     |            |                           |            |                              |
     -----------------------            -----------------------------            --------------------------------

- Stylování pojištìní
   - Detail - Pøidána tabulka pojistných událostí - OK

- Stylování pojistných událostí
   - Index - seznam všech pojistných událostí - OK
   - Create - Nová pojistná událost - OK
   - Detail - Údaje o pojistné události - OK
   - Edit - Editace pojistné události - OK
   - Delete - Smazání pojistné události - OK

- Sjednotit dialogy pojištìncù, pojištìní, událostí
   - Index - OK
   - Detail - OK
   - Edit - OK
   - Create - OK
   - Delete - OK

- Do dialogù doplnit èíslo pojistné smlouvy a èíslo pojistné události - OK

- Sjednotit do funkcí ukládání pomocných údajù do viewBagu

  ---------------------------------------------
- Registrace a role
  ---------------------------------------------
   - Aplikace podporuje uživatelské role (pojištìný, administrátor).
     Navrhni a implementuj, kdo vidí a mùže editovat jaká data.
   
      - Pojištìný
         - Vidí pouze svá pojištìní a události, nemùže nic editovat.
   
      - Administrátor
         - Vidí vše a mùže aplikaci používat v plném rozsahu.
   

   - Nastavení Identity v Program.cs (E-shop v ASP.NET Lekce 4)

      - Zmìníme volání builder.Services.AddDefaultIdentity() na builder.Services.AddIdentity()
      - Pøidáme registraci Razor stránek pomocí builder.Services.AddRazorPages().
   
   - Registrace administrátorského úètu (E-shop v ASP.NET Lekce 9)

      - V aplikaci budeme na mnoha místech rozlišovat roli administrátora (správce) a bìžného 
        uživatele. Proto bychom mìli mít vždy k dispozici nìjaký výchozí administrátorský úèet, 
        tedy uživatele s rolí Admin. Pro jeho registraci si vytvoøíme rozšiøující metodu tøídy 
        WebApplication, kterou budeme volat v Program.cs ještì pøed spuštìním aplikace.

      - V projektu si nejprve vytvoøíme novou složku Extensions/ a do ní pøidáme: 
         - Statickou tøídu WebApplicationExtensions
         - Pomocnou metodu CreateUser()
         - Metoda RegisterAdmin()

      - V souboru Program.cs voláme fci RegisterAdmin. Protože je asynchronní, je nutné mìnit
        public static void Main(string[] args) na
        public static async Task Main(string[] args)

   - Pøidání view modelù RegisterViewModel, LoginViewModel, ChangePasswordViewModel: 
      - Do složky Models pøidáme nové tøídy RegisterViewModel, LoginViewModel a ChangePasswordViewModel
      - Základy lekce 13, 14, Eshop lekce 9

   - Vytvoøení controlleru "AccountController.cs"
      - Pravým na složku "Controllers" -> Add -> Controller -> MVC Controller - Empty
      - Do controlleru doplnit jednotlivé akce, viz:
        Základy lekce 13, 14, Eshop lekce 10
   
   - Vytvoøení pohledù Register.cshtml, Login.cshtml, ChangePassword.cshtml
      - Pravým do akce AccountController.Login -> Add View -> Razor View - Empty
      - Do View doplnit jejich obsah, viz:
        Základy lekce 13, 14, Eshop lekce 11

   - Migrace databáze - postup viz výše

   - Do controllerù PersonsController, InsurencesController, IncidentsController
     pøidány atributy [Authorize] a [Authorize(Roles = "Admin")], které zpøístupòují controllery
     a jednotlivé akce buï administrátorovi nebo pøihlášeným uživatelùm (Základy lekce 15)

   - Dorùzných pohledù pøidána podmínka @if (User.IsInRole("Admin")), která zobrazuje nìkterá
     editaèní tlaèítka pouze administrátorovi (Základy lekce 15)

  

---------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------
Dotazy:

 - Je správné øešení ukládání PersonId do TempData v PersonsController.Details a jeho opìtovné
   použití v [HttpPost]InsurancesController.Create JK???
    - Ano, toto øešení je správné

 - Jak na zvýraznìní aktuální položky v navigaci?
   https://stackoverflow.com/questions/3643613/asp-net-mvc-highlighting-current-page-link-technique
   https://sirwan.info/blog/en/highlighting-current-link-in-razor-pages
   
 - Problém s validací decimal hodnoty:
    - Zkusit tam dát kontrolu regulárního výrazu:
      https://stackoverflow.com/questions/49556755/regularexpression-validation-attribute-not-working-correctly
    - Zkusil jsem ten regulární výraz a stejnì to nefunguje :-(

 - Paginace
   https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application


#################################################################################################*/


using Insurance_ASP.Data;
using Insurance_ASP.Extensions;        // RegisterAdmin
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Insurance_ASP
{
    //#############################################################################################
    public class Program
    {
        //-----------------------------------------------------------------------------------------
        // Fce Main upravena na asynchronní, protoøe v ní voláme asynchronní fci RegisterAdmin.
        //-----------------------------------------------------------------------------------------
        public static async Task Main(string[] args)  
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .UseLazyLoadingProxies());        // Pøidáno kvùli automatické naplnìní navigaèních vlastností v modelech
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Nastavení Identity (E-shop v ASP.NET Lekce 4)
            //  - Zmìníme volání builder.Services.AddDefaultIdentity() na
            //    builder.Services.AddIdentity(),
            //  - Nastavíme minimální délku hesla na 8 znakù,
            //  - Vypneme povinnost použití nealfanumerických znakù.
            //  - Nastavíme na false obvyklé potvrzení emailové adresy, které v naší aplikaci dále nebudeme vyžadovat,
            //  - Nastavíme na true použití unikátní e - mailové adresy,
            //  - Pøidáme registraci Razor stránek pomocí builder.Services.AddRazorPages().
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /* Pùvodní nahrazeno novým viz výše
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            */

            builder.Services.AddControllersWithViews();

            // Pøidáno v souvislosti s nastavením identity (E-shop v ASP.NET Lekce 4)
            builder.Services.AddRazorPages();  

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // Pøidáno kvùli registraci úètu administrátora (E-shop v ASP.NET Lekce 9)
            await app.RegisterAdmin("admin@email.cz", "Admin123");  

            app.Run();
        }
    }
}