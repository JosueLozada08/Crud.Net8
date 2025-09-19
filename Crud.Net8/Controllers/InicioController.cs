using System.Diagnostics;
using Crud.Net8.Models;
using Microsoft.AspNetCore.Mvc;
using Crud.Net8.Datos;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Contacto.ToArrayAsync());
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
