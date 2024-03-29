/*#################################################################################################
Insurance_ASP - Z�v�re�n� projekt
###################################################################################################

- Zalo�en� projektu
   - Podle �ablony ASP.NET Core Web App (Model-View-Controller)
   - Verzi frameworku vybereme .NET 6.0.
   - U v�b�ru typu ov��en� nastav�me Individual Accounts
   - Za�krtneme "Do not use top-level statements"

- Nainstalov�n� nugetu Microsoft.EntityFrameworkCore.Proxies
   - Jak se to d�l� je pops�no v t�to lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni
   - Nuget je pot�eba k tomu, aby prob�hal automatick� lazy loading naviga�n�ch vlastnost� v
     modelech slou��c�ch k vazb�m mezi entitami.

- Pou�it� .UseLazyLoadin�gProxies() 
   - V souboru program.cs, jak je pops�no v lekci 5:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-migrace-a-prvni-spusteni
   - Je pot�eba ze stejn�ch d�vod� jako nuget Microsoft.EntityFrameworkCore.Proxies v��e.

- Vypnut� nullovateln�ch referen�n�ch typ�
   - Vypneme smaz�n�m ��dku <Nullable>enable</Nullable> v elementu <PropertyGroup>, kter� 
     nalezneme v aktu�ln� otev�en�m .csproj (otev�eme poklep�n�m na hlavn� slo�ku projektu
     v solution exploreru)
   - Jak se to d�l� je pops�no v t�to lekci 2:
     https://www.itnetwork.cz/csharp/asp-net-core/eshop-zaklad/e-shop-v-aspnet-zaklad-priprava-a-struktura-reseni

- Do slo�ky wwwroot p�id�me novou slo�ku "images", do kter� budeme ukl�dat p��padn� obr�zky.

- Vytvo�en� datov�ho modelu "Person"
   - Do slo�ky Models p�id�me novou t��du "Person".

- Vytvo�en� controlleru "PersonsController.cs"
   - prav�m na slo�ku "Controllers" -> Add -> Controller -> 
     -> MVC Controller with views, using Entity Framework -> Model class "Person" ->
     -> Data context class "ApplicationDbContext" -> od�ktrnout "Reference script libraries" -> Add
   - T�m se v ApplicationDbContext.cs p�id� ��dek:
     public DbSet<Insurance_ASP.Models.Person>? Person { get; set; }
     Ten definuje, �e se ze t��dy "Person" p�i migraci datab�ze vytvo�� datab�zov� tabulka.

- Provedena migrace datab�ze (P�id�n� tabulky Person)
   - Rebuild
   - Roleta Tools -> NuGet Package Manager -> Package Manager Console -> Do konzole napsat:
     Add-Migration Create_Table_Person
     Update-Database
   - Datab�ze je um�st�na v adres��i C:\Users\Admin\ jako soubor .mdf

- Vytvo�en� datov�ho modelu "Insurance"
   - Do slo�ky Models p�id�me novou t��du "Insurance".

- �prava t��dy ApplicationDbContext:
   - P�esat metodu OnModelCreating a v n� atributu Amount v tabulce Insurance, 
     kter� je typu decimal nastav�me p�esnost.

- �prava t��dy Person
   - Kv�li vazb� 1: N mezi entitami Person |- --<| Insurance nutno p�idat vlastnost:
     public virtual ICollection<Insurance> Insurances

- Vytvo�en� controlleru "InsurancesController.cs"
   - Analogicky jako u "PersonsController.cs"

- Provedena migrace datab�ze (P�id�n� tabulky Insurance a update tabulky Person)
   - Rebuild
   - Add-Migration Create_Table_Insurance
   - Update-Database
   - Na�e datab�ze nyn� vypad� n�jak takto:

     -----------------------            -----------------------------
     | Person              |            | Insurance                 |
     |---------------------|            |---------------------------|
     | #* Id (PK not-null) |            | #* Id (PK not-null)       |
     |    Dal�� parametry  |- - - -----<|  * PersonId (FK not-null) |
     |                     |            |    Dal�� parametry        |
     |                     |            |                           |
     -----------------------            -----------------------------

- Stylov�n� poji�t�nc�
   - Index - seznam v�ech poji�t�nc� - OK
   - Create - Nov� poji�t�nec - OK
   - Detail - �daje o poji�t�nci v�etn� sjednan�ch poji�t�n� - OK
   - Edit - Editace poji�t�nce - OK
   - Delete - Smaz�n� poji�t�nce - OK

- Stylov�n� poji�t�n�
   - Index - seznam v�ech poji�t�n� - OK
   - Create - Nov� poji�t�n� - OK
   - Detail - �daje o poji�t�n� - OK
   - Edit - Editace poji�t�n� - OK
   - Delete - Smaz�n� poji�t�n� - OK

- Datov� �pravy poji�t�n�:
   - Datum a �as nahradit pouze datumem - OK
     Vy�e�eno pou�it�m atributu [DataType(DataType.Date)]

- Stylov�n� navigace, z�pat� a hlavn� str�nky

- Vytvo�en� datov�ho modelu "Incident" na pojistn� ud�losti
   - Do slo�ky Models p�id�me novou t��du "Incident".

- �prava t��dy ApplicationDbContext:
   - Do metody OnModelCreating a do n� p�idat Amount v tabulce Incident, 
     kter� je typu decimal nastav�me p�esnost.

- �prava t��dy Insurance
   - Kv�li vazb� 1: N mezi entitami Insurance |- --<| Incident nutno p�idat vlastnost:
     public virtual ICollection<Incident> Incidents

- Vytvo�en� controlleru "IncidentsController.cs"
   - Analogicky jako u "PersonsController.cs"

- Provedena migrace datab�ze (P�id�n� tabulky Incident a update tabulky Person)
   - Rebuild
   - Add-Migration Create_Table_Insurance
   - Update-Database
   - Na�e datab�ze nyn� vypad� n�jak takto:

     -----------------------            -----------------------------            --------------------------------
     | Person              |            | Insurance                 |            | Incident                     |
     |---------------------|            |---------------------------|            |------------------------------|
     | #* Id (PK not-null) |            | #* Id (PK not-null)       |            | #* Id (PK not-null)          |
     |    Dal�� parametry  |- - - -----<|  * PersonId (FK not-null) |- - - -----<|  * InsuranceId (FK not-null) |
     |                     |            |    Dal�� parametry        |            |    Dal�� parametry           |
     |                     |            |                           |            |                              |
     -----------------------            -----------------------------            --------------------------------

- Stylov�n� poji�t�n�
   - Detail - P�id�na tabulka pojistn�ch ud�lost� - OK

- Stylov�n� pojistn�ch ud�lost�
   - Index - seznam v�ech pojistn�ch ud�lost� - OK
   - Create - Nov� pojistn� ud�lost - OK
   - Detail - �daje o pojistn� ud�losti - OK
   - Edit - Editace pojistn� ud�losti - OK
   - Delete - Smaz�n� pojistn� ud�losti - OK

- Sjednotit dialogy poji�t�nc�, poji�t�n�, ud�lost�
   - Index - OK
   - Detail - OK
   - Edit - OK
   - Create - OK
   - Delete - OK

- Do dialog� doplnit ��slo pojistn� smlouvy a ��slo pojistn� ud�losti - OK

- Sjednotit do funkc� ukl�d�n� pomocn�ch �daj� do viewBagu

  ---------------------------------------------
- Registrace a role
  ---------------------------------------------
   - Aplikace podporuje u�ivatelsk� role (poji�t�n�, administr�tor).
     Navrhni a implementuj, kdo vid� a m��e editovat jak� data.
   
      - Poji�t�n�
         - Vid� pouze sv� poji�t�n� a ud�losti, nem��e nic editovat.
   
      - Administr�tor
         - Vid� v�e a m��e aplikaci pou��vat v pln�m rozsahu.
   

   - Nastaven� Identity v Program.cs (E-shop v ASP.NET Lekce 4)

      - Zm�n�me vol�n� builder.Services.AddDefaultIdentity() na builder.Services.AddIdentity()
      - P�id�me registraci Razor str�nek pomoc� builder.Services.AddRazorPages().
   
   - Registrace administr�torsk�ho ��tu (E-shop v ASP.NET Lekce 9)

      - V aplikaci budeme na mnoha m�stech rozli�ovat roli administr�tora (spr�vce) a b�n�ho 
        u�ivatele. Proto bychom m�li m�t v�dy k dispozici n�jak� v�choz� administr�torsk� ��et, 
        tedy u�ivatele s rol� Admin. Pro jeho registraci si vytvo��me roz�i�uj�c� metodu t��dy 
        WebApplication, kterou budeme volat v Program.cs je�t� p�ed spu�t�n�m aplikace.

      - V projektu si nejprve vytvo��me novou slo�ku Extensions/ a do n� p�id�me: 
         - Statickou t��du WebApplicationExtensions
         - Pomocnou metodu CreateUser()
         - Metoda RegisterAdmin()

      - V souboru Program.cs vol�me fci RegisterAdmin. Proto�e je asynchronn�, je nutn� m�nit
        public static void Main(string[] args) na
        public static async Task Main(string[] args)

   - P�id�n� view model� RegisterViewModel, LoginViewModel, ChangePasswordViewModel: 
      - Do slo�ky Models p�id�me nov� t��dy RegisterViewModel, LoginViewModel a ChangePasswordViewModel
      - Z�klady lekce 13, 14, Eshop lekce 9

   - Vytvo�en� controlleru "AccountController.cs"
      - Prav�m na slo�ku "Controllers" -> Add -> Controller -> MVC Controller - Empty
      - Do controlleru doplnit jednotliv� akce, viz:
        Z�klady lekce 13, 14, Eshop lekce 10
   
   - Vytvo�en� pohled� Register.cshtml, Login.cshtml, ChangePassword.cshtml
      - Prav�m do akce AccountController.Login -> Add View -> Razor View - Empty
      - Do View doplnit jejich obsah, viz:
        Z�klady lekce 13, 14, Eshop lekce 11

   - Migrace datab�ze - postup viz v��e

   - Do controller� PersonsController, InsurencesController, IncidentsController
     p�id�ny atributy [Authorize] a [Authorize(Roles = "Admin")], kter� zp��stup�uj� controllery
     a jednotliv� akce bu� administr�torovi nebo p�ihl�en�m u�ivatel�m (Z�klady lekce 15)

   - Dor �zn�ch pohled� p�id�na podm�nka @if (User.IsInRole("Admin")), kter� zobrazuje n�kter�
     edita�n� tla��tka pouze administr�torovi (Z�klady lekce 15)

   - Za��dit, aby pokud je n�kdo p�ihl�en jako b�n� u�ivatel (nikoliv administr�tor) vid�l
     pouze sv� �daje o poji�t�nci, poji�t�n� a pojistn�ch ud�lostech.
  
      - Ve view Person/Index.cshtml, Insurances/Index.cshtml, Incidents/Index.cshtml je v cyklu
        tvo��c�m ��dky tabulky p�id�na filtruj�c� podm�nka porovn�vaj�c� email p�ihl�en�ho
        u�ivatele a email poj�t�nce z db tabulky Person.
        
      - V akc�ch PersonsController.Details, InsurancesController.Details, 
        IncidentsController.Details p�id�na podobn� omezuj�c� podm�nka jako ve views (bod v��e).

   - Za��dit, aby se p�i registraci nov�ho u�ivatele z�rove� vytvo�il i nov� jemu odpov�daj�c�
     poji�t�nec se shodn�m emailem

      - Upraveno view Account/Register.cshtml, kam se p�idaly v�echny vstupy nutn� pro vytvo�en�
        poji�t�nce (t��da Person)

      - Upravena akce AccountController.Register, kde se krom� registrace u�ivatele vytvo��
        jemu odpov�daj�c� poji�t�nec (t��da Person) a vlo�� se do datab�ze.

   - Za��dit, aby se p�i vytvo�en� nov�ho poji�t�nce (Preson) z�rove� zaregistroval nov� u�ivatel
     se shodn�m emailem

      - Upraveno view Person/Create.cshtml, kam se p�idaly v�echny vstupy nutn� pro vytvo�en�
        u�ivatele, tj heslo.

      - Upravena akce PersonsController/Create, kde se krom� poji�t�nce vytvo�� jemu odpov�daj�c�
        u�ivatel se stejn�m emailem.

      - Proveden� migrace datab�ze - postup viz v��e.

  ---------------------------------------------
- Paginace
  ---------------------------------------------

   - Paginace je zapracovan� podle tohoto vzoru: 
     https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-6.0

   - P�id�n soubor PaginatedList s t��dou PaginatedList

   - V controllerech PersonsController, InsurancesController, IncidentsController upravena akce
     Index, do kter� je p�id�n parametr pageNumber a pou�it model PaginatedList

   - Upravena view Person/Index, Insurances/Index, Incidents/Index:
      - Jako model je m�sto IEnumerable pou�it PaginatedList
      - Je dopln�na sekce s Paginac� (stanoven� prom�nn�ch prevDisabled, nextDisabled) a tla��tka
        P�edchoz� a Dal��

  --------------------------------------------
- Napln�n� pr�zdn� datab�ze testovac�mi daty
  --------------------------------------------
   - Zaji��uje to funkce SeedEmptyDatabase
   - Inspirov�no funkc� RegisterAdmin

  ------------------------------------------------------------------
- Zajistit, aby se p�i smaz�n� poji�t�nce smazal i odpov�daj�c� user
  ------------------------------------------------------------------
   - https://www.yogihosting.com/aspnet-core-identity-create-read-update-delete-users/
   - �prava v akci PersonsController.DeleteConfirmed

  --------------------------------------------------------------------
- Zajistit, aby se p�i editaci poji�t�nce a p��padn� zm�n� jeho emailu  
  zm�nil email a username odpov�daj�c�ho usera
  --------------------------------------------------------------------
   - https://www.yogihosting.com/aspnet-core-identity-create-read-update-delete-users/
   - �prava v [Get] a [HttpPost] akci PersonsController.Edit
   - �prava ve view Persons/Edit.cshtml
      - Nastaveny defaultn� hodnoty Password a ConfirmPassword, viz komant�� tam.

  -------------------
- launchSettings.json
  -------------------
   - P�id�n blok pro publikov�n� a spou�t�n� na webu
      - Pokud aplikaci publikujeme, je pot�eba m�t tento blok odkomentovan� a blok
        pro lok�ln� spou�t�n� zakomentovan�.
      - Pokud chceme aplikaci sputit lok�ln�, tak naopak.

  ----------------
- appsettings.json
  ----------------
   - P�id�n connection string pro publikov�n� a spou�t�n� na webu
      - Pokud aplikaci publikujeme, je pot�eba m�t tento connection string odkomentovan� a 
        connection string pro pro lok�ln� spu�t�n� zakomentovan�.
      - Pokud chceme aplikaci sputit lok�ln�, tak naopak.


  --------------------------
- P�id�na str�nka O aplikaci
  --------------------------
   - Do HomeControlleru p�id�na akce About
   - P�id�no view Views\Home\About.cshtml
   - Do navigace ve Views\Shared\_Layout.cshtml p�id�n odkaz na st�nku O aplikaci
   - Na �vodn� str�nku ve Views\Home\Index.cshtml p�id�n odkaz na st�nku O aplikaci (Jak za��t?)

  ----------------------------------------------
- Znemo�n�n� zm�ny hesla administr�torsk�ho ��tu
  ----------------------------------------------
   - Ve Views\Shared\_LoginPartial.csthml p�id�na podm�nka znemo��uj�c� zm�nu hesla 
     administr�torsk�ho ��tu  

###################################################################################################
Zprovozn�n� projektu
###################################################################################################

Klonov�n� repozit��e z GitHubu

 - Nap��klad pomoc� TCMD si vytvo��me n�jak� adres��, do kter�ho chceme repozit�� naklonovat.

 - Jdeme do toho adres��e -> prav�m -> Git Bash here -> 
   git clone https://github.com/JaromirKrizek/ITnetwork_Project.git 

 - Obsah repozit��e z githubu se tam nakop�ruje.

---------------------------------------------------------------------------------------------------
Migrace datab�ze po naklonov�n� repozit��e

 - Roleta Tools -> NuGet Package Manager -> Package Manager Console -> Do konzole napsat:
   Add-Migration Update_Database_XX
   Update-Database
 
 - Datab�ze je pak um�st�na v adres��i C:\Users\Admin\ jako soubor .mdf

---------------------------------------------------------------------------------------------------
P��padn� smaz�n� datab�ze

 - Roleta View -> SQL Server Object Explorer -> SQL Server -> LocalDB -> Databases ->
   -> prav�m na n�zev datab�ze 
   -> Delete -> za�krtnout delete backup ... 
                za�krtnout close existing connections.

---------------------------------------------------------------------------------------------------
P�ihl�en� jako admin:
admin@email.cz  
Admin123

P�ihl�en� jako dal�� u�ivatel�:
jan.novak@email.cz, Jan.novak0
petr.benda@email.cz, Petr.benda0
frantisek.fiala@email.cz, Frantisek.fiala0
kvetoslav.zapadak@email.cz, Kvetoslav.zapadak0

Viz Extensions\WebApplicationExtensions.SeedEmptyDatabase

###################################################################################################
Dotazy:
###################################################################################################

 - Je spr�vn� �e�en� ukl�d�n� PersonId do TempData v PersonsController.Details a jeho op�tovn�
   pou�it� v [HttpPost]InsurancesController.Create JK???
    - Ano, toto �e�en� je spr�vn�

 - Jak na zv�razn�n� aktu�ln� polo�ky v navigaci?
   https://stackoverflow.com/questions/3643613/asp-net-mvc-highlighting-current-page-link-technique
   https://sirwan.info/blog/en/highlighting-current-link-in-razor-pages
   
 - Probl�m s validac� decimal hodnoty:
    - Zkusit tam d�t kontrolu regul�rn�ho v�razu:
      https://stackoverflow.com/questions/49556755/regularexpression-validation-attribute-not-working-correctly
    - Zkusil jsem ten regul�rn� v�raz a stejn� to nefunguje :-(

 - Paginace
   https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application


 - Konzultace st�eda 21.9.2022

    - Pokud bych cht�l pou��t n�jak� enum:
      prom�nn� = Enum.GetValues(typeof(MyEnumType));

    - Pokud bych cht�l vyu��t jedno view na dv� akce, 
      nap��klad Persons\Create.cshtml a Account\Register.cshtml jsou stejn�, 
      m��u nap��klad v AccountController.Register m�sto return View(); zavolat
      return RedirectToAction("Create", "Person");
      Akci lze tak� p�idat n�jak� parametr podle toho odkud bylo view vol�no.

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
        // Fce Main upravena na asynchronn�, proto�e v n� vol�me asynchronn� fci RegisterAdmin.
        //-----------------------------------------------------------------------------------------
        public static async Task Main(string[] args)  
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .UseLazyLoadingProxies());        // P�id�no kv�li automatick� napln�n� naviga�n�ch vlastnost� v modelech
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Nastaven� Identity (E-shop v ASP.NET Lekce 4)
            //  - Zm�n�me vol�n� builder.Services.AddDefaultIdentity() na
            //    builder.Services.AddIdentity(),
            //  - Nastav�me minim�ln� d�lku hesla na 8 znak�,
            //  - Vypneme povinnost pou�it� nealfanumerick�ch znak�.
            //  - Nastav�me na false obvykl� potvrzen� emailov� adresy, kter� v na�� aplikaci d�le nebudeme vy�adovat,
            //  - Nastav�me na true pou�it� unik�tn� e - mailov� adresy,
            //  - P�id�me registraci Razor str�nek pomoc� builder.Services.AddRazorPages().
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            /* P�vodn� nahrazeno nov�m viz v��e
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            */

            builder.Services.AddControllersWithViews();

            // P�id�no v souvislosti s nastaven�m identity (E-shop v ASP.NET Lekce 4)
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Po��te�n� inicializace datab�ze:
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

            // P�id�no kv�li registraci ��tu administr�tora (E-shop v ASP.NET Lekce 9)
            await app.RegisterAdmin("admin@email.cz", "Admin123");

            // Pokud je datab�ze pr�zdn�, napln� ji po��te�n�mi daty.
            await app.SeedEmptyDatabase();

            app.Run();
        }
    }
}