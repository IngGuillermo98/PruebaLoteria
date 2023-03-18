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
    public class CartasPorTablasController : Controller
    {
        private readonly DBContext _context;

        public CartasPorTablasController(DBContext context)
        {
            _context = context;
        }

        // GET: CartasPorTablas
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.CartasPorTabla.Include(c => c.IdCartaNavigation).Include(c => c.IdTablaNavigation);
            return View(await dBContext.ToListAsync());
        }

        // GET: CartasPorTablas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartasPorTabla == null)
            {
                return NotFound();
            }

            var cartasPorTabla = await _context.CartasPorTabla
                .Include(c => c.IdCartaNavigation)
                .Include(c => c.IdTablaNavigation)
                .FirstOrDefaultAsync(m => m.IdCartasPorTabla == id);
            if (cartasPorTabla == null)
            {
                return NotFound();
            }

            return View(cartasPorTabla);
        }

        // GET: CartasPorTablas/Create
        public IActionResult Create()
        {
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta");
            ViewData["IdTabla"] = new SelectList(_context.Tablas, "IdTabla", "IdTabla");
            return View();
        }

        // POST: CartasPorTablas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCartasPorTabla,IdCarta,IdTabla,PosicionF,PosicionC")] CartasPorTabla cartasPorTabla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartasPorTabla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", cartasPorTabla.IdCarta);
            ViewData["IdTabla"] = new SelectList(_context.Tablas, "IdTabla", "IdTabla", cartasPorTabla.IdTabla);
            return View(cartasPorTabla);
        }

        // GET: CartasPorTablas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartasPorTabla == null)
            {
                return NotFound();
            }

            var cartasPorTabla = await _context.CartasPorTabla.FindAsync(id);
            if (cartasPorTabla == null)
            {
                return NotFound();
            }
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", cartasPorTabla.IdCarta);
            ViewData["IdTabla"] = new SelectList(_context.Tablas, "IdTabla", "IdTabla", cartasPorTabla.IdTabla);
            return View(cartasPorTabla);
        }

        // POST: CartasPorTablas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCartasPorTabla,IdCarta,IdTabla,PosicionF,PosicionC")] CartasPorTabla cartasPorTabla)
        {
            if (id != cartasPorTabla.IdCartasPorTabla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartasPorTabla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartasPorTablaExists(cartasPorTabla.IdCartasPorTabla))
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
            ViewData["IdCarta"] = new SelectList(_context.Cartas, "IdCarta", "IdCarta", cartasPorTabla.IdCarta);
            ViewData["IdTabla"] = new SelectList(_context.Tablas, "IdTabla", "IdTabla", cartasPorTabla.IdTabla);
            return View(cartasPorTabla);
        }

        // GET: CartasPorTablas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartasPorTabla == null)
            {
                return NotFound();
            }

            var cartasPorTabla = await _context.CartasPorTabla
                .Include(c => c.IdCartaNavigation)
                .Include(c => c.IdTablaNavigation)
                .FirstOrDefaultAsync(m => m.IdCartasPorTabla == id);
            if (cartasPorTabla == null)
            {
                return NotFound();
            }

            return View(cartasPorTabla);
        }

        // POST: CartasPorTablas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartasPorTabla == null)
            {
                return Problem("Entity set 'DBContext.CartasPorTabla'  is null.");
            }
            var cartasPorTabla = await _context.CartasPorTabla.FindAsync(id);
            if (cartasPorTabla != null)
            {
                _context.CartasPorTabla.Remove(cartasPorTabla);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartasPorTablaExists(int id)
        {
          return (_context.CartasPorTabla?.Any(e => e.IdCartasPorTabla == id)).GetValueOrDefault();
        }
    }
}
