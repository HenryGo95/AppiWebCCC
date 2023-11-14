using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppiC.Models;

namespace WebAppiC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult AgregarLibro()
        {
            return View();
        }

        public IActionResult ModificarLibro()
        {
            return View();
        }

        public IActionResult EliminarLibro()
        {
            return View();
        }

        public IActionResult ConsultarLibro()
        {
            return View();
        }

        public IActionResult NuevoUsuario()
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
