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
    public class IncidentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        //-----------------------------------------------------------------------------------------
        public IncidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //-----------------------------------------------------------------------------------------
        // GET: Incidents
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Index(int? pageNumber)  // pageNumber přidáno pro paginaci
        {
            var query = _context.Incident.Include(i => i.Insurance);
            var model = await PaginatedList<Incident>.CreateAsync(query, pageNumber ?? 1, pageSize: 10);
            return View(model);

            // Původní provedení před zavedením paginace
            // var applicationDbContext = _context.Incident.Include(i => i.Insurance);
            // return View(await applicationDbContext.ToListAsync());
        }

        //-----------------------------------------------------------------------------------------
        // GET: Incidents/Details/5
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Incident == null)
            {
                return NotFound();
            }

            var incident = await _context.Incident
                .Include(i => i.Insurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            // Pokud je přihlášen běžný uživatel a jeho email se neshoduje s emailem aktuálně
            // volaného uživatele, přesměruje se na výchozí stránku. (Přidáno JK???)
            if (!User.IsInRole("Admin") && User.Identity.Name != incident.Insurance.Person.Email)
            {
                return RedirectToAction("Index", "Incidents");
            }

            // Nutno zjistit pojištěnce a pojištění této události pro potřeby view (Přidáno JK???)
            ViewBag.Insurance = incident.Insurance;
            ViewBag.Person = incident.Insurance.Person;

            return View(incident);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Incidents/Create
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public IActionResult Create()
        {
            //ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Id", "Subject");

            // Z TempData získá insuranceId, které bylo uloženo v InsurancesController.Details.
            // Dle něj najde v databázi pojištění a pojištěnce vloží je do viewBagu pro účely view.
            // (Přidáno JK???)
            if (TempData.ContainsKey("InsuranceId"))
            {
                int insuranceId = Convert.ToInt32(TempData["InsuranceId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var insurance = _context.Insurance.Find(insuranceId);
                int personId = insurance.PersonId;
                var person = _context.Person.Find(personId);

                ViewBag.Insurance = insurance;
                ViewBag.Person = person;
            }
            
            return View();
        }

        //-----------------------------------------------------------------------------------------
        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Date,Status,Amount,InsuranceId")] Incident incident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Z TempData získá insuranceId, které bylo uloženo v InsurancesController.Details.
            // Dle něj najde v databázi pojištění a pojištěnce vloží je do viewBagu pro účely view.
            // (Přidáno JK???)
            if (TempData.ContainsKey("InsuranceId"))
            {
                int insuranceId = Convert.ToInt32(TempData["InsuranceId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var insurance = await _context.Insurance.FindAsync(insuranceId);
                int personId = insurance.PersonId;
                var person = await _context.Person.FindAsync(personId);

                ViewBag.Insurance = insurance;
                ViewBag.Person = person;
            }

            //ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Id", "Subject", incident.InsuranceId);
            return View(incident);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Incidents/Edit/5
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Incident == null)
            {
                return NotFound();
            }

            var incident = await _context.Incident.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }

            // Uloží si InsuranceId do TempData pro pozdější použití v
            // [HttpPost]IcidentsController.Edit při editaci události (Přidáno JK???)
            TempData["InsuranceId"] = incident.Insurance.Id;
            TempData["PersonId"] = incident.Insurance.Person.Id;

            // Nutno zjistit pojištěnce a pojištění této události pro potřeby view (Přidáno JK???)
            ViewBag.Insurance = incident.Insurance;
            ViewBag.Person = incident.Insurance.Person;

            //ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Id", "Subject", incident.InsuranceId);
            return View(incident);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Date,Status,Amount,InsuranceId")] Incident incident)
        {
            if (id != incident.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentExists(incident.Id))
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

            // Z TempData získá insuranceId, které bylo uloženo v InsurancesController.Details.
            // Dle něj najde v databázi pojištění a pojištěnce vloží je do viewBagu pro účely view.
            // (Přidáno JK???)
            if (TempData.ContainsKey("InsuranceId"))
            {
                int insuranceId = Convert.ToInt32(TempData["InsuranceId"].ToString());

                // Způsobí, aby hodnota v TempData zůstala i po zavolání dalších akcí.
                TempData.Keep();

                var insurance = await _context.Insurance.FindAsync(insuranceId);
                int personId = insurance.PersonId;
                var person = await _context.Person.FindAsync(personId);

                ViewBag.Insurance = insurance;
                ViewBag.Person = person;
            }

            //ViewData["InsuranceId"] = new SelectList(_context.Insurance, "Id", "Subject", incident.InsuranceId);
            return View(incident);
        }

        //-----------------------------------------------------------------------------------------
        // GET: Incidents/Delete/5
        //-----------------------------------------------------------------------------------------
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Incident == null)
            {
                return NotFound();
            }

            var incident = await _context.Incident
                .Include(i => i.Insurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incident == null)
            {
                return NotFound();
            }

            // Nutno zjistit pojištěnce a pojištění této události pro potřeby view (Přidáno JK???)
            ViewBag.Insurance = incident.Insurance;
            ViewBag.Person = incident.Insurance.Person;

            return View(incident);
        }

        //-----------------------------------------------------------------------------------------
        // POST: Incidents/Delete/5
        //-----------------------------------------------------------------------------------------
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]  // K této akci má přístup pouze admin
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Incident == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Incident'  is null.");
            }
            var incident = await _context.Incident.FindAsync(id);
            if (incident != null)
            {
                _context.Incident.Remove(incident);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //-----------------------------------------------------------------------------------------
        private bool IncidentExists(int id)
        {
          return _context.Incident.Any(e => e.Id == id);
        }
    }
}
