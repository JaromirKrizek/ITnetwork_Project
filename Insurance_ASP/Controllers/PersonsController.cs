using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Insurance_ASP.Data;
using Insurance_ASP.Models;
using Microsoft.AspNetCore.Authorization;  // [Authorize]
using Microsoft.AspNetCore.Identity;       // UserManager

namespace Insurance_ASP.Controllers
{
    //#############################################################################################
    [Authorize]  // K akcím tohoto controlleru mají přístup pouze přihlášení uživatelé
    public class PersonsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        //-----------------------------------------------------------------------------------------
        public PersonsController(ApplicationDbContext context,
                                 UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //-----------------------------------------------------------------------------------------
        // GET: Persons
        // Výpis všech pojištěnců
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Index(int? pageNumber)  // pageNumber přidáno pro paginaci 
        {
            var query = _context.Person;
            if (query != null)
            {
                // PaginatedList zaveden kvůli paginaci ve view
                var model = await PaginatedList<Person>.CreateAsync(query, pageNumber ?? 1, pageSize: 10);
                return View(model);
            }
            else
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }

            // Původní provedení před zavedením paginace
            // Původní provedení před zavedením paginace
            // return _context.Person != null ?
            //             View(await _context.Person.ToListAsync()) :
            //             Problem("Entity set 'ApplicationDbContext.Person'  is null.");
        }

        //-----------------------------------------------------------------------------------------
        // GET: Persons/Details/5
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            // Pokud je přihlášen běžný uživatel a jeho email se neshoduje s emailem aktuálně
            // volaného uživatele, přesměruje se na výchozí stránku. (Přidáno JK???)
            if (!User.IsInRole("Admin") && User.Identity.Name != person.Email)
            {
                return RedirectToAction("Index", "Persons");
            }

            // Uloží si PersonId do TempData pro pozdější použití v
            // [HttpPost]InsurancesController.Create při tvorbě pojištění (Přidáno JK???)
            TempData["PersonId"] = person.Id;

            return View(person);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Persons/Create
        // Vytvoření pojištěnce
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public IActionResult Create()
        {
            return View();
        }

        //-----------------------------------------------------------------------------------------
        // POST: Persons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Phone,Street,HouseNumber,City,PostCode,Password,ConfirmPassword")] Person person)
        {
            // IsValid ověří, zda je Person platný. Přitom bere v úvahu i validace nastavené
            // v modelové třídě Article.
            if (ModelState.IsValid)
            {
                // Pokud nový uživatel ještě není zaregistrovaný
                if (await _userManager.FindByEmailAsync(person.Email) is null)
                {
                    // Uživatele tvoříme jako novou instanci třídy IdentityUser.
                    var user = new IdentityUser { UserName = person.Email, Email = person.Email };

                    // Vytvoříme nového uživatele pomocí metody CreateAsync() na instanci
                    // třídy UserManager.
                    // Přitom se zkontroluje platnost hesla a pokud proběhne v pořádku,
                    // vloží uživatele do databáze (tabulka dbo.AspNetUsers):
                    var result = await _userManager.CreateAsync(user, person.Password);

                    // Pokud vytvoření uživatele proběhlo v pořádku
                    if (result.Succeeded)
                    {
                        // Do databázové tabulky dbo.Person vloží nový záznam.
                        _context.Add(person);
                        await _context.SaveChangesAsync();

                        // Zavolá akci PersonsController.Index() - Návrat na výpis všech pojištěnců
                        return RedirectToAction(nameof(Index));
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
                        { Description = $"Email {person.Email} je již zaregistrován" }));
                }

            }
            return View(person);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Persons/Edit/5
        // Editace pojištěnce
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            // Získává pojištěnce z databázové tabulky dbo.Person jako objekt třídy Person na
            // základě parametru id:
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Persons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,Phone,Street,HouseNumber,City,PostCode")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Updatuje záznam v databázové tabulce dbo.Person:
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Persons/Delete/5
        // Odstranění pojištěnce
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Persons/Delete/5
        //-----------------------------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                // Odstraní záznam z databázové tabulky dbo.Person
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //-----------------------------------------------------------------------------------------
        private bool PersonExists(int id)
        {
            return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
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

    }
}
