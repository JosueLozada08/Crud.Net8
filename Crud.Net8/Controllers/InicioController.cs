using System.Diagnostics;
using Crud.Net8.Models;
using Microsoft.AspNetCore.Mvc;
using Crud.Net8.Datos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Crud.Net8.Controllers
{
    public class InicioController : Controller

    {
        private readonly AplicationDbContext _contexto;

        public InicioController(AplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        // GET: /Inicio
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datos = await _contexto.Contacto
                .AsNoTracking()
                .OrderBy(c => c.Nombre)
                .ToListAsync();

            return View(datos);
        }

        // GET: /Inicio/Detalle/5
        [HttpGet]
        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null) return NotFound();

            var contacto = await _contexto.Contacto
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contacto == null) return NotFound();

            return View(contacto);
        }

        // GET: /Inicio/Crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /Inicio/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([Bind("Nombre,Celular,Email")] Contacto contacto)
        {
            if (!ModelState.IsValid) return View(contacto);

            contacto.FechaCreacion = DateTime.Now; // set en servidor
            _contexto.Add(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: /Inicio/Editar/5
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null) return NotFound();

            var contacto = await _contexto.Contacto.FindAsync(id);
            if (contacto == null) return NotFound();

            return View(contacto);
        }

        // POST: /Inicio/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Nombre,Celular,Email,FechaCreacion")] Contacto contacto)
        {
            if (id != contacto.Id) return NotFound();
            if (!ModelState.IsValid) return View(contacto);

            try
            {
                // Seguimos el patrón Attach + State.Modified para actualizar
                _contexto.Entry(contacto).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _contexto.Contacto.AnyAsync(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Inicio/Borrar/5  (pantalla de confirmación)
        [HttpGet]
        public async Task<IActionResult> Borrar(int? id)
        {
            if (id == null) return NotFound();

            var contacto = await _contexto.Contacto
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (contacto == null) return NotFound();

            return View(contacto);
        }

        // POST: /Inicio/Borrar/5  (confirma y elimina)
        [HttpPost, ActionName("Borrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarConfirmado(int id)
        {
            var contacto = await _contexto.Contacto.FindAsync(id);
            if (contacto == null) return NotFound();

            _contexto.Contacto.Remove(contacto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Extras por plantilla MVC
        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
