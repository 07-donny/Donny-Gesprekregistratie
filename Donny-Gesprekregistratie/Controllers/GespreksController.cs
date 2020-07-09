using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Donny_Gesprekregistratie.Data;
using Donny_Gesprekregistratie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Donny_Gesprekregistratie.Controllers
{
    [Authorize]
    public class GespreksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public GespreksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Gespreks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gesprek.Include(g => g.Reacties).ThenInclude(m => m.Medewerker).ToListAsync());
        }

        // GET: Gespreks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gesprek = await _context.Gesprek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gesprek == null)
            {
                return NotFound();
            }

            return View(gesprek);
        }

        // GET: Gespreks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gespreks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Voornaam,Achternaam,Datum,Melding,Onderwerp,Inhoud,Afgesloten")] Gesprek gesprek)
        {
            gesprek.Medewerker = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                _context.Add(gesprek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gesprek);
        }

        // GET: Gespreks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gesprek = await _context.Gesprek.FindAsync(id);
            if (gesprek == null)
            {
                return NotFound();
            }
            return View(gesprek);
        }

        // POST: Gespreks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Voornaam,Achternaam,Datum,Melding,Onderwerp,Inhoud,Afgesloten")] Gesprek gesprek)
        {
            if (id != gesprek.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gesprek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GesprekExists(gesprek.Id))
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
            return View(gesprek);
        }

        // GET: Gespreks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gesprek = await _context.Gesprek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gesprek == null)
            {
                return NotFound();
            }

            return View(gesprek);
        }

        // POST: Gespreks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gesprek = await _context.Gesprek.FindAsync(id);
            _context.Gesprek.Remove(gesprek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GesprekExists(int id)
        {
            return _context.Gesprek.Any(e => e.Id == id);
        }
    }
}