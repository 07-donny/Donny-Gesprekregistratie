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
    public class ReactiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReactiesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reacties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reactie.Include(r => r.Gesprek).ThenInclude(g => g.Medewerker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reacties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactie = await _context.Reactie
                .Include(r => r.Gesprek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reactie == null)
            {
                return NotFound();
            }

            return View(reactie);
        }

        // GET: Reacties/Create
        public IActionResult Create(int? gespreksId)
        {
            ViewData["GesprekId"] = gespreksId;
            return View();
        }

        // POST: Reacties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Datum,Inhoud,GesprekId")] Reactie reactie)
        {
            reactie.Datum = DateTime.Now;
            reactie.Medewerker = await _userManager.GetUserAsync(HttpContext.User);

            if (ModelState.IsValid)
            {
                _context.Add(reactie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GesprekId"] = new SelectList(_context.Gesprek, "Id", "Id", reactie.GesprekId);
            return View(reactie);
        }

        // GET: Reacties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactie = await _context.Reactie.FindAsync(id);
            if (reactie == null)
            {
                return NotFound();
            }
            ViewData["GesprekId"] = new SelectList(_context.Gesprek, "Id", "Id", reactie.GesprekId);
            return View(reactie);
        }

        // POST: Reacties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Datum,Inhoud,GesprekId")] Reactie reactie)
        {
            if (id != reactie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reactie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReactieExists(reactie.Id))
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
            ViewData["GesprekId"] = new SelectList(_context.Gesprek, "Id", "Id", reactie.GesprekId);
            return View(reactie);
        }

        // GET: Reacties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reactie = await _context.Reactie
                .Include(r => r.Gesprek)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reactie == null)
            {
                return NotFound();
            }

            return View(reactie);
        }

        // POST: Reacties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reactie = await _context.Reactie.FindAsync(id);
            _context.Reactie.Remove(reactie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReactieExists(int id)
        {
            return _context.Reactie.Any(e => e.Id == id);
        }
    }
}