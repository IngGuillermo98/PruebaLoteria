using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoteriaMexicana.Models.Context;
using LoteriaMexicana.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LoteriaMexicana.Controllers
{
    public class TablasController : Controller
    {
        private readonly DBContext _context;

        public TablasController(DBContext context)
        {
            _context = context;
        }

        // GET: Tablas
        public async Task<IActionResult> Index()
        {
            var tablas = await _context.Tablas
                .Include(t => t.CartasPorTabla)
                .ThenInclude(c => c.IdCartaNavigation)
                .ToListAsync();
            return View(tablas);
        }

        // GET: Tablas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tablas = await _context.Tablas
                .FirstOrDefaultAsync(m => m.IdTabla == id);
            if (tablas == null)
            {
                return NotFound();
            }

            return View(tablas);
        }

        // GET: Tablas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTabla,Nombre")] Tablas tablas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tablas);
        }

        // GET: Tablas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tablas = await _context.Tablas.FindAsync(id);
            if (tablas == null)
            {
                return NotFound();
            }
            return View(tablas);
        }

        // POST: Tablas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTabla,Nombre")] Tablas tablas)
        {
            if (id != tablas.IdTabla)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TablasExists(tablas.IdTabla))
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
            return View(tablas);
        }

        // GET: Tablas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tablas == null)
            {
                return NotFound();
            }

            var tablas = await _context.Tablas
                .FirstOrDefaultAsync(m => m.IdTabla == id);
            if (tablas == null)
            {
                return NotFound();
            }

            return View(tablas);
        }

        // POST: Tablas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tablas == null)
            {
                return Problem("Entity set 'DBContext.Tablas'  is null.");
            }
            var tablas = await _context.Tablas.FindAsync(id);
            if (tablas != null)
            {
                _context.Tablas.Remove(tablas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TablasExists(int id)
        {
            return (_context.Tablas?.Any(e => e.IdTabla == id)).GetValueOrDefault();
        }
        //================================================================================
        [HttpPost]
        public ActionResult generarTablas(int cantidad)
        {
            try
            {
                _context.Tablas.RemoveRange(_context.Tablas);
                _context.CartasPorTabla.RemoveRange(_context.CartasPorTabla);
                _context.SaveChanges();

                // Generar una lista de nombres y descripciones únicas para cada tablero
                List<string> nombres = new List<string>();
                //List<string> descripciones = new List<string>();
                for (int i = 0; i < cantidad; i++)
                {
                    string nombre = "";
                    //string descripcion = "";
                    do
                    {
                        nombre = "Tablero " + (i + 1);
                    } while (nombres.Contains(nombre));
                    nombres.Add(nombre);
                    //descripcion = "Descripción del tablero " + (i + 1);
                    //descripciones.Add(descripcion);

                    // Verificar si el nombre del tablero ya existe en la base de datos
                    if (!_context.Tablas.Any(t => t.Nombre == nombre))
                    {
                        // Ejecutar el stored procedure para agregar un nuevo registro
                        _context.Database.ExecuteSqlRaw("EXEC spAgregarTabla {0}", nombre);
                    }
                }
                _context.SaveChanges(); // Guardar los cambios en la base de datos
                List<Cartas> cartas = _context.Cartas.ToList();
                Random random = new Random(); // Creamos una instancia de la clase Random


                // Obtener los 4 tableros existentes en la base de datos
                var Tablas = _context.Tablas.ToList();

                foreach (var t in Tablas)
                {
                    List<Cartas> cartasUtilizadas = new List<Cartas>(); // Crear una lista de cartas utilizadas en este tablero
                    for (int i = 0; i < 16; i++)
                    {
                        Cartas cartaAleatoria;
                        do
                        {
                            cartaAleatoria = cartas[random.Next(cartas.Count)]; // Obtener una carta aleatoria de la lista de cartas
                        } while (cartasUtilizadas.Contains(cartaAleatoria)); // Verificar que la carta no se haya utilizado previamente en este tablero
                        cartasUtilizadas.Add(cartaAleatoria); // Agregar la carta utilizada a la lista de cartas utilizadas en este tablero
                        var Id_Carta = cartaAleatoria.IdCarta;
                        int fila = random.Next(1, 5);
                        int columna = random.Next(1, 5);

                        // Ejecutar el stored procedure para agregar una nueva celda
                        _context.Database.ExecuteSqlRaw("EXEC sp_InsertarCartasPorTabla {0}, {1}, {2}, {3}", Id_Carta, t.IdTabla, fila, columna);
                    }
                }
                return Json(new { success = true, message = "Se generaron los tableros correctamente" });
            }
            catch (Exception ex)
            {
                // Manejar la excepción aquí, por ejemplo, registrarla en un archivo de registro o mostrar un mensaje de error al usuario
                return Json(new { success = false, message = "Ocurrió un error al generar los tableros: " + ex.Message });
            }
        }


    }
}
