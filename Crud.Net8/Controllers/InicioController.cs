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


        //private readonly ILogger<InicioController> _logger;

        public InicioController(AplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Contacto.ToArrayAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _contexto.Contacto.Add(contacto);
                await _contexto.SaveChangesAsync();//se convierte en un metodo asincrono y se guarda 
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
