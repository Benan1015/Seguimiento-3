using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteItaliano.Data;
using RestauranteItaliano.Models;

namespace RestauranteItaliano.Controllers
{
    public class PlatillosController : Controller
    {
        private readonly RestauranteDbContext _context;

        public PlatillosController(RestauranteDbContext context)
        {
            _context = context;
        }

        // READ (Consultar todos los platillos junto con su categoría relacionada)
        public async Task<IActionResult> Index()
        {
            var platillos = await _context.Platillos
                .Include(p => p.Categoria)
                .ToListAsync();

            return View(platillos);
        }

        // CREATE - GET (Carga el select con las categorías)
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoriaId = new SelectList(
                await _context.Categorias.ToListAsync(),
                "Id",
                "Nombre"
            );
            return View();
        }

        // CREATE - POST (Guarda en SQL Server)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Platillo platillo)
        {
            if (ModelState.IsValid)
            {
                _context.Platillos.Add(platillo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(
                await _context.Categorias.ToListAsync(),
                "Id",
                "Nombre",
                platillo.CategoriaId
            );
            return View(platillo);
        }

        // EDIT - GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var platillo = await _context.Platillos.FindAsync(id);
            if (platillo == null) return NotFound();

            ViewBag.CategoriaId = new SelectList(
                await _context.Categorias.ToListAsync(),
                "Id",
                "Nombre",
                platillo.CategoriaId
            );
            return View(platillo);
        }

        // EDIT - POST (Actualiza en SQL Server)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Platillo platillo)
        {
            if (id != platillo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(platillo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(
                await _context.Categorias.ToListAsync(),
                "Id",
                "Nombre",
                platillo.CategoriaId
            );
            return View(platillo);
        }

        // DELETE - GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var platillo = await _context.Platillos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (platillo == null) return NotFound();

            return View(platillo);
        }

        // DELETE - POST (Elimina de SQL Server)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var platillo = await _context.Platillos.FindAsync(id);
            if (platillo != null)
            {
                _context.Platillos.Remove(platillo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}