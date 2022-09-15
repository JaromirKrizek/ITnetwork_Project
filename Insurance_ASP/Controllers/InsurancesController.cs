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

namespace Insurance_ASP.Controllers
{
    //#############################################################################################
    [Authorize]  // K akcím tohoto controlleru mají přístup pouze přihlášení uživatelé
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        //-----------------------------------------------------------------------------------------
        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //-----------------------------------------------------------------------------------------
        // GET: Insurances
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Insurance.Include(i => i.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        //-----------------------------------------------------------------------------------------
        // GET: Insurances/Details/5
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            // Pokud je přihlášen běžný uživatel a jeho email se neshoduje s emailem aktuálně
            // volaného uživatele, přesměruje se na výchozí stránku. (Přidáno JK???)
            if (!User.IsInRole("Admin") && User.Identity.Name != insurance.Person.Email)
            {
                return RedirectToAction("Index", "Insurances");
            }

            // Nutno zjistit pojištěnce tohoto pojištění pro potřeby view (Přidáno JK???)
            ViewBag.Person = insurance.Person;

            // Uloží si InsuranceId do TempData pro pozdější použití v
            // [HttpPost]IcidentsController.Create při tvorbě události (Přidáno JK???)
            TempData["InsuranceId"] = insurance.Id;

            return View(insurance);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Insurances/Create
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public IActionResult Create()
        {
            //ViewData["PersonId"] = new SelectList(_context.Person, "Id", "City");  // Původní JK!!!
            //ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Id");  // Nové JK!!!

            // Z TempData získá personId, které bylo uloženo v PersonsController.Details,
            // Podle něj najde v databázi osobu a tu vloží do viewBagu pro účely view.
            // (Přidáno JK???)
            if (TempData.ContainsKey("PersonId"))
            {
                int personId = Convert.ToInt32(TempData["PersonId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var person = _context.Person.Find(personId);
                ViewBag.Person = person;
            }

            return View();
        }

        //-----------------------------------------------------------------------------------------
        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Amount,Subject,ValidFrom,ValidTo,PersonId")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Z TempData získá personId, které bylo uloženo v PersonsController.Details,
            // Podle personId najde v databázi osobu a tu vloží do viewBagu pro účely view (Přidáno JK???)
            if (TempData.ContainsKey("PersonId"))
            {
                int personId = Convert.ToInt32(TempData["PersonId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var person = await _context.Person.FindAsync(personId);
                ViewBag.Person = person;
            }

            // ViewData["PersonId"] = new SelectList(_context.Person, "Id", "Id");  // Nové JK!!!
            // ViewData["PersonId"] = new SelectList(_context.Person, "Id", "City", insurance.PersonId);  // Původní JK!!!
            return View(insurance);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Insurances/Edit/5
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }

            // Uloží si PersonId do TempData pro pozdější použití v
            // [HttpPost]InsurancesController.Edit při editaci pojištění (Přidáno JK???)
            TempData["PersonId"] = insurance.Person.Id;

            // Nutno zjistit pojištěnce tohoto pojištění pro potřeby view (Přidáno JK???)
            ViewBag.Person = insurance.Person;

            // ViewData["PersonId"] = new SelectList(_context.Person, "Id", "City", insurance.PersonId);
            return View(insurance);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Amount,Subject,ValidFrom,ValidTo,PersonId")] Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.Id))
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

            // Z TempData získá personId, které bylo uloženo v PersonsController.Details,
            // Podle personId najde v databázi osobu a tu vloží do viewBagu pro účely view (Přidáno JK???)
            if (TempData.ContainsKey("PersonId"))
            {
                int personId = Convert.ToInt32(TempData["PersonId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var person = await _context.Person.FindAsync(personId);
                ViewBag.Person = person;
            }

            // ViewData["PersonId"] = new SelectList(_context.Person, "Id", "City", insurance.PersonId);
            return View(insurance);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Insurances/Delete/5
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }

            // Nutno zjistit pojištěnce tohoto pojištění pro potřeby view (Přidáno JK???)
            var person = await _context.Person.FindAsync(insurance.PersonId);
            if (person == null)
            {
                return NotFound();
            }
            ViewBag.Person = person;

            return View(insurance);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Insurances/Delete/5
        //-----------------------------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insurance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurance'  is null.");
            }
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurance.Remove(insurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //-----------------------------------------------------------------------------------------
        private bool InsuranceExists(int id)
        {
          return (_context.Insurance?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //-----------------------------------------------------------------------------------------

    }
}
