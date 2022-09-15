using Microsoft.AspNetCore.Authorization;  // [Authorize]
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;       // UserManager, SignInManager
using Insurance_ASP.Models;                // LoginViewModel
using Insurance_ASP.Data;                  // ApplicationDbContext

namespace Insurance_ASP.Controllers
{
    //#############################################################################################
    // Controller pro přihlašovací a registrační formulář (lekce 13, 14) - Vytvořen jako Empty
    // Zpracovává tyto modely
    //  - LoginViewModel
    //  - RegisterViewModel
    // Jsou na něj navázána tato view
    //  - Login.csthml
    //  - Register.csthml
    //#############################################################################################
    [Authorize]  // Zajišťuje, že se k akcím této třídy dostanou pouze přihlášení uživatelé
    public class AccountController : Controller
    {
        // Ćlenské proměnné nutné pro účely přihlašování
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        //-----------------------------------------------------------------------------------------
        // Konstruktor - Instance služeb nám budou do parametrů konstruktoru předány zcela automaticky,
        // což je mechanismus Dependency Injection. Služby, které ve svých aplikacích potřebujeme,
        // se do kontrolerů nesnažíme nějak sami předávat, ale pouze si je vyžádáme v konstruktoru.
        // ASP.NET Core vše ostatní vyřeší za nás. Služby musí být registrovány v Program.cs, což je
        // již automaticky uděláno a to díky šabloně projektu, ktrou jsme zvolili na začátku
        // při vytvoření projektu.
        //-----------------------------------------------------------------------------------------
        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //-----------------------------------------------------------------------------------------
        // Pomocná metoda (akce) pro zjištění, zda návratová adresa je lokální adresou.
        // Pokud ano, přesměrujeme na ni, pokud ne přesměrujeme na výchozí akci Index v
        // kontroleru HomeController
        //-----------------------------------------------------------------------------------------
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                // return RedirectToAction("Index", "Home");  // Alternativně
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        //-----------------------------------------------------------------------------------------
        // Pomocná metoda pro odeslání chyb, zjištěných při validaci, zpátky do View
        // Eshop lekce 10
        //-----------------------------------------------------------------------------------------
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        //-----------------------------------------------------------------------------------------
        // Akce pro registraci vyvolává view Register - Lekce 14
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;  // Návratovou stránku si uložíme do dat controlleru
            return View();
        }

        //-----------------------------------------------------------------------------------------
        // Akce zpracovávající registrační formulář - Lekce 14, Eshop lekce 10
        // V HttpPost variantě zpracujeme vyplněný registrační formulář.
        // Po zvalidování dat z formuláře vytvoříme nového uživatele pomocí metody CreateAsync()
        // na instanci třídy UserManager:
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Pokud nový uživatel ještě není zaregistrovaný
                if (await _userManager.FindByEmailAsync(model.Email) is null)
                {
                    // Uživatele tvoříme jako novou instanci třídy IdentityUser.
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email };

                    // Vytvoříme nového uživatele pomocí metody CreateAsync() na instanci
                    // třídy UserManager.
                    // Přitom se zkontroluje platnost hesla a pokud proběhne v pořádku,
                    // vloží uživatele do databáze (tabulka dbo.AspNetUsers):
                    var result = await _userManager.CreateAsync(user, model.Password);

                    // Pokud vytvoření uživatele proběhlo v pořádku
                    if (result.Succeeded)
                    {
                        // Vytvoří nového pojištěnce
                        var person = new Person { Id = 0,
                                                  FirstName = model.FirstName,
                                                  LastName = model.LastName,
                                                  Email = model.Email,
                                                  Phone = model.Phone,
                                                  Street = model.Street,
                                                  HouseNumber = model.HouseNumber,
                                                  City = model.City,
                                                  PostCode = model.PostCode,
                                                  Insurances = null };

                        // Do databázové tabulky dbo.Person vloží nového pojištěnce.
                        _context.Add(person);
                        await _context.SaveChangesAsync();

                        // Tato metoda uživatele rovnou i přihlásí.
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }

                    foreach (var error in result.Errors)
                    {
                        // Do view přidá varovné hlášky informující o tom,
                        // že ověření hesla neproběhlo úspěšně:
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    AddErrors(IdentityResult.Failed(new IdentityError() 
                                 { Description = $"Email {model.Email} je již zaregistrován" }));
                }
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k registračnímu formuláři
            return View(model);
        }

        //-----------------------------------------------------------------------------------------
        // V GET metodě pouze vrátíme příslušný pohled s formulářem.
        // Jelikož se v aplikaci může stát, že uživatel byl na přihlášení přesměrován z důvodu,
        // že neměl práva na nějakou stránku, lze akci předat i stránku, na kterou má být uživatel
        // po přihlášení zas navrácen (string returnUrl).
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]  // Zjistí, že k akci mají přístup i nepřihlášení uživatelé
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;  // Návratovou stránku si uložíme do dat controlleru
            return View();
        }

        //-----------------------------------------------------------------------------------------
        // Akce zpracovávající přihlašovací formulář.
        // POST metoda se pokusí uživatele přihlásit, jelikož bude reagovat na situaci,
        // kdy byl formulář odeslaný.
        //
        // await: jedná se o syntaxi pro volání tzv. asynchronních metod.
        // To jsou metody, které nevrátí svou hodnotu ihned, ale až po nějakém časovém intervalu.
        // Mezitím program na návratovou hodnotu volané metody nečeká, ale pokračuje dále.
        // My ovšem na výsledek počkat potřebujeme, a proto čekání vynutíme klíčovým slovem await.
        // Více o tomto tématu se dočtete v kurzu Paralelní programování a vícevláknové aplikace v
        // C# .NET: https://www.itnetwork.cz/csharp/vlakna
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string navratovaURL = null)
        {
            ViewData["ReturnUrl"] = navratovaURL;

            if (ModelState.IsValid)
            {
                // Pokus o přihlášení uživatele na základě zadaných údajů.
                // Ověření uživatele provedeme metodou PasswordSignInAsync() instance třídy SignInManager:
                // await vynucuje vyčkání na dokončení asynchronní metody PasswordSignInAsync. 
                var vysledekOvereni = await _signInManager.PasswordSignInAsync(model.Email, model.Password,
                                                                               model.RememberMe,
                                                                               lockoutOnFailure: false);
                if (vysledekOvereni.Succeeded)
                {
                    // Jednoduchá pomocná metoda uvedená níže
                    return RedirectToLocal(navratovaURL);
                }
                else
                {
                    // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři.
                    // Do view přidá varovné hlášky informující o tom, že ověření hesla neproběhlo úspěšně:
                    ModelState.AddModelError(string.Empty, "Neplatné přihlašovací údaje.");
                    return View(model);
                }
            }

            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }

        //-----------------------------------------------------------------------------------------
        // Akce pro odhlášení.
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        [Authorize]  
        public async Task<IActionResult> Logout()
        {
            // Využijeme instanci třídy SignInManager a její metodu SignOutAsync() pro odhlášení:
            // await vynucuje vyčkání na dokončení asynchronní metody PasswordSignInAsync.
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
            // return RedirectToAction("Index", "Home");  // Alternativně
        }

        //-----------------------------------------------------------------------------------------
        // Akce pro změnu hesla - Eshop lekce 10
        // V [HttpGet] variantě akce zobrazíme formulář pro změnu hesla.
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User) ??
                throw new ApplicationException($"Nepodařilo se načíst uživatele s ID {_userManager.GetUserId(User)}.");

            var model = new ChangePasswordViewModel();
            return View(model);
        }

        //-----------------------------------------------------------------------------------------
        // Akce pro změnu hesla - Eshop lekce 10
        // V [HttpPost] variantě akce zkontrolujeme data z příchozího modelu, změníme heslo,
        // přihlásíme a přesměrujeme uživatele na výchozí stránku
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User) ??
                throw new ApplicationException($"Nepodařilo se načíst uživatele s ID: {_userManager.GetUserId(User)}.");

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
            //return RedirectToAction("Administration");
        }

        //-----------------------------------------------------------------------------------------

    }
}
