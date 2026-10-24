using Microsoft.AspNetCore.Mvc;
using RestauranteItaliano.Models;

namespace RestauranteItaliano.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // ACCIÓN GET: Muestra el formulario vacío al usuario
        [HttpGet]
        public IActionResult Reserva()
        {
            return View();
        }

        // ACCIÓN POST: Recibe y procesa los datos del formulario en C#
        [HttpPost]
        public IActionResult Reserva(Reserva modeloReserva)
        {
            if (ModelState.IsValid)
            {
                // Mensaje procesado en el servidor que se envía a la vista
                ViewBag.MensajeExito = $"¡Reserva confirmada con éxito para {modeloReserva.NombreCliente} en la zona {modeloReserva.Zona}!";
            }

            return View(modeloReserva);
        }
    }
}