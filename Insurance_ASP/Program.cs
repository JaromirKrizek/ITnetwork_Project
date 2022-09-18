/*#################################################################################################
Insurance_ASP - Závìreènı projekt
###################################################################################################

- Zaloení projektu
   - Podle šablony ASP.NET Core Web App (Model-View-Controller)
   - Verzi frameworku vybereme .NET 6.0.
   - U vıbìru typu ovìøení nastavíme Individual Accounts
   - Zaškrtneme "Do not use top-level statements"

- Nainstalování nugetu Microsoft.EntityFrameworkCore.Proxies
   - Jak se to dìlá je popsáno v této lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni
   - Nuget je potøeba k tomu, aby probíhal automatickı lazy loading navigaèních vlastností v
     modelech slouících k vazbám mezi entitami.

- Pouití .UseLazyLoadin­gProxies() 
   - V souboru program.cs, jak je popsáno v lekci 5:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-migrace-a-prvni-spusteni
   - Je potøeba ze stejnıch dùvodù jako nuget Microsoft.EntityFrameworkCore.Proxies vıše.

- Vypnutí nullovatelnıch referenèních typù
   - Vypneme smazáním øádku <Nullable>enable</Nullable> v elementu <PropertyGroup>, kterı 
     nalezneme v aktuálnì otevøeném .csproj (otevøeme poklepáním na hlavní sloku projektu
     v solution exploreru)
   - Jak se to dìlá je popsáno v této lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni

- Do sloky wwwroot pøidáme novou sloku "images", do které budeme ukládat pøípadné obrázky.

- Vytvoøení datového modelu "Person"
   - Do sloky Models pøidáme novou tøídu "Person".

- Vytvoøení controlleru "PersonsController.cs"
   - pravım na sloku "Controllers" -> Add -> Controller -> 
     -> MVC Controller with views, using Entity Framework -> Model class "Person" ->
     -> Data context class "ApplicationDbContext" -> odšktrnout "Reference script libraries" -> Add
   - Tím se v ApplicationDbContext.cs pøidá øádek:
     public DbSet<Insurance_ASP.Models.Person>? Person { get; set; }
     Ten definuje, e se ze tøídy "Person" pøi migraci databáze vytvoøí databázová tabulka.

- Provedena migrace databáze (Pøidání tabulky Person)
   - Rebuild
   - Roleta Tools -> NuGet Package Manager -> Package Manager Console -> Do konzole napsat:
     Add-Migration Create_Table_Person
     Update-Database
   - Databáze je umístìna v adresáøi C:\Users\Admin\ jako soubor .mdf

- Vytvoøení datového modelu "Insurance"
   - Do sloky Models pøidáme novou tøídu "Insurance".

- Úprava tøídy ApplicationDbContext:
   - Pøesat metodu OnModelCreating a v ní atributu Amount v tabulce Insurance, 
     kterı je typu decimal nastavíme pøesnost.

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
   - Create - Novı pojištìnec - OK
   - Detail - Údaje o pojištìnci vèetnì sjednanıch pojištìní - OK
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
     Vyøešeno pouitím atributu [DataType(DataType.Date)]

- Stylování navigace, zápatí a hlavní stránky

- Vytvoøení datového modelu "Incident" na pojistné události
   - Do sloky Models pøidáme novou tøídu "Incident".

- Úprava tøídy ApplicationDbContext:
   - Do metody OnModelCreating a do ní pøidat Amount v tabulce Incident, 
     kterı je typu decimal nastavíme pøesnost.

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
   - Detail - Pøidána tabulka pojistnıch událostí - OK

- Stylování pojistnıch událostí
   - Index - seznam všech pojistnıch událostí - OK
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

- Sjednotit do funkcí ukládání pomocnıch údajù do viewBagu

  ---------------------------------------------
- Registrace a role
  ---------------------------------------------
   - Aplikace podporuje uivatelské role (pojištìnı, administrátor).
     Navrhni a implementuj, kdo vidí a mùe editovat jaká data.
   
      - Pojištìnı
         - Vidí pouze svá pojištìní a události, nemùe nic editovat.
   
      - Administrátor
         - Vidí vše a mùe aplikaci pouívat v plném rozsahu.
   

   - Nastavení Identity v Program.cs (E-shop v ASP.NET Lekce 4)

      - Zmìníme volání builder.Services.AddDefaultIdentity() na builder.Services.AddIdentity()
      - Pøidáme registraci Razor stránek pomocí builder.Services.AddRazorPages().
   
   - Registrace administrátorského úètu (E-shop v ASP.NET Lekce 9)

      - V aplikaci budeme na mnoha místech rozlišovat roli administrátora (správce) a bìného 
        uivatele. Proto bychom mìli mít vdy k dispozici nìjakı vıchozí administrátorskı úèet, 
        tedy uivatele s rolí Admin. Pro jeho registraci si vytvoøíme rozšiøující metodu tøídy 
        WebApplication, kterou budeme volat v Program.cs ještì pøed spuštìním aplikace.

      - V projektu si nejprve vytvoøíme novou sloku Extensions/ a do ní pøidáme: 
         - Statickou tøídu WebApplicationExtensions
         - Pomocnou metodu CreateUser()
         - Metoda RegisterAdmin()

      - V souboru Program.cs voláme fci RegisterAdmin. Protoe je asynchronní, je nutné mìnit
        public static void Main(string[] args) na
        public static async Task Main(string[] args)

   - Pøidání view modelù RegisterViewModel, LoginViewModel, ChangePasswordViewModel: 
      - Do sloky Models pøidáme nové tøídy RegisterViewModel, LoginViewModel a ChangePasswordViewModel
      - Základy lekce 13, 14, Eshop lekce 9

   - Vytvoøení controlleru "AccountController.cs"
      - Pravım na sloku "Controllers" -> Add -> Controller -> MVC Controller - Empty
      - Do controlleru doplnit jednotlivé akce, viz:
        Základy lekce 13, 14, Eshop lekce 10
   
   - Vytvoøení pohledù Register.cshtml, Login.cshtml, ChangePassword.cshtml
      - Pravım do akce AccountController.Login -> Add View -> Razor View - Empty
      - Do View doplnit jejich obsah, viz:
        Základy lekce 13, 14, Eshop lekce 11

   - Migrace databáze - postup viz vıše

   - Do controllerù PersonsController, InsurencesController, IncidentsController
     pøidány atributy [Authorize] a [Authorize(Roles = "Admin")], které zpøístupòují controllery
     a jednotlivé akce buï administrátorovi nebo pøihlášenım uivatelùm (Základy lekce 15)

   - Dor ùznıch pohledù pøidána podmínka @if (User.IsInRole("Admin")), která zobrazuje nìkterá
     editaèní tlaèítka pouze administrátorovi (Základy lekce 15)

   - Zaøídit, aby pokud je nìkdo pøihlášen jako bìnı uivatel (nikoliv administrátor) vidìl
     pouze své údaje o pojištìnci, pojištìní a pojistnıch událostech.
  
      - Ve view Person/Index.cshtml, Insurances/Index.cshtml, Incidents/Index.cshtml je v cyklu
        tvoøícím øádky tabulky pøidána filtrující podmínka porovnávající email pøihlášeného
        uivatele a email pojštìnce z db tabulky Person.
        
      - V akcích PersonsController.Details, InsurancesController.Details, 
        IncidentsController.Details pøidána podobná omezující podmínka jako ve views (bod vıše).

   - Zaøídit, aby se pøi registraci nového uivatele zároveò vytvoøil i novı jemu odpovídající
     pojištìnec se shodnım emailem

      - Upraveno view Account/Register.cshtml, kam se pøidaly všechny vstupy nutné pro vytvoøení
        pojištìnce (tøída Person)

      - Upravena akce AccountController.Register, kde se kromì registrace uivatele vytvoøí
        jemu odpovídající pojištìnec (tøída Person) a vloí se do databáze.

   - Zaøídit, aby se pøi vytvoení nového pojištìnce (Preson) zároveò zaregistroval novı uivatel
     se shodnım emailem

      - Upraveno view Person/Create.cshtml, kam se pøidaly všechny vstupy nutné pro vytvoøení
        uivatele, tj heslo.

      - Upravena akce PersonsController/Create, kde se kromì pojištìnce vytvoøí jemu odpovídající
        uivatel se stejnım emailem.

      - Provedení migrace databáze - postup viz vıše.

  ---------------------------------------------
- Paginace
  ---------------------------------------------

   - Paginace je zapracovaná podle tohoto vzoru: 
     https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0

   - Pøidán soubor PaginatedList s tøídou PaginatedList

   - V controllerech PersonsController, InsurancesController, IncidentsController upravena akce
     Index, do které je pøidán parametr pageNumber a pouit model PaginatedList

   - Upravena view Person/Index, Insurances/Index, Incidents/Index:
      - Jako model je místo IEnumerable pouit PaginatedList
      - Je doplnìna sekce s Paginací (stanovení promìnnıch prevDisabled, nextDisabled) a tlaèítka
        Pøedchozí a Další

  --------------------------------------------
- Naplnìní prázdné databáze testovacími daty
  --------------------------------------------
   - Zajišuje to funkce SeedEmptyDatabase
   - Inspirováno funkcí RegisterAdmin

  ------------------------------------------------------------------
- Zajistit, aby se pøi smazání pojištìnce smazal i odpovídající user
  ------------------------------------------------------------------
   - https://www.yogihosting.com/aspnet-core-identity-create-read-update-delete-users/
   - Úprava v akci PersonsController.DeleteConfirmed

  --------------------------------------------------------------------
- Zajistit, aby se pøi editaci pojištìnce a pøípadné zmìnì jeho emailu  
  zmìnil email a username odpovídajícího usera
  --------------------------------------------------------------------
   - https://www.yogihosting.com/aspnet-core-identity-create-read-update-delete-users/
   - Úprava v [Get] a [HttpPost] akci PersonsController.Edit
   - Úprava ve view Persons/Edit.cshtml
      - Nastaveny defaultní hodnoty Password a ConfirmPassword, viz komantáø tam.

---------------------------------------------------------------------------------------------------
Dotazy:

 - Je správné øešení ukládání PersonId do TempData v PersonsController.Details a jeho opìtovné
   pouití v [HttpPost]InsurancesController.Create JK???
    - Ano, toto øešení je správné

 - Jak na zvıraznìní aktuální poloky v navigaci?
   https://stackoverflow.com/questions/3643613/asp-net-mvc-highlighting-current-page-link-technique
   https://sirwan.info/blog/en/highlighting-current-link-in-razor-pages
   
 - Problém s validací decimal hodnoty:
    - Zkusit tam dát kontrolu regulárního vırazu:
      https://stackoverflow.com/questions/49556755/regularexpression-validation-attribute-not-working-correctly
    - Zkusil jsem ten regulární vıraz a stejnì to nefunguje :-(

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
            //  - Vypneme povinnost pouití nealfanumerickıch znakù.
            //  - Nastavíme na false obvyklé potvrzení emailové adresy, které v naší aplikaci dále nebudeme vyadovat,
            //  - Nastavíme na true pouití unikátní e - mailové adresy,
            //  - Pøidáme registraci Razor stránek pomocí builder.Services.AddRazorPages().
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /* Pùvodní nahrazeno novım viz vıše
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            */

            builder.Services.AddControllersWithViews();

            // Pøidáno v souvislosti s nastavením identity (E-shop v ASP.NET Lekce 4)
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Poèáteèní inicializace databáze:
            // https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-6.0&tabs=visual-studio
            
            // using (var scope = app.Services.CreateScope())
            // {
            //     var services = scope.ServiceProvider;
            //     SeedData.Initialize(services);
            // }

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

            // Pokud je databáze prázdná, naplní ji poèáteèními daty.
            await app.SeedEmptyDatabase();

            app.Run();
        }
    }
}