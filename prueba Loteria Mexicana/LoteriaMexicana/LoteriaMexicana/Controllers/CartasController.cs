using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoteriaMexicana.Models.Context;
using LoteriaMexicana.Models.Entities;

namespace LoteriaMexicana.Controllers
{
    public class CartasController : Controller
    {
        private readonly DBContext _context;

        public CartasController(DBContext context)
        {
            _context = context;
        }

        // GET: Cartas
        public async Task<IActionResult> Index()
        {
              return _context.Cartas != null ? 
                          View(await _context.Cartas.ToListAsync()) :
                          Problem("Entity set 'DBContext.Cartas'  is null.");
        }

        // GET: Cartas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var cartas = await _context.Cartas
                .FirstOrDefaultAsync(m => m.IdCarta == id);
            if (cartas == null)
            {
                return NotFound();
            }

            return View(cartas);
        }

        // GET: Cartas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cartas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarta,Nombre")] Cartas cartas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cartas);
        }

        // GET: Cartas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var cartas = await _context.Cartas.FindAsync(id);
            if (cartas == null)
            {
                return NotFound();
            }
            return View(cartas);
        }

        // POST: Cartas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarta,Nombre")] Cartas cartas)
        {
            if (id != cartas.IdCarta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartasExists(cartas.IdCarta))
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
            return View(cartas);
        }

        // GET: Cartas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cartas == null)
            {
                return NotFound();
            }

            var cartas = await _context.Cartas
                .FirstOrDefaultAsync(m => m.IdCarta == id);
            if (cartas == null)
            {
                return NotFound();
            }

            return View(cartas);
        }

        // POST: Cartas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cartas == null)
            {
                return Problem("Entity set 'DBContext.Cartas'  is null.");
            }
            var cartas = await _context.Cartas.FindAsync(id);
            if (cartas != null)
            {
                _context.Cartas.Remove(cartas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartasExists(int id)
        {
          return (_context.Cartas?.Any(e => e.IdCarta == id)).GetValueOrDefault();
        }
    }
}
