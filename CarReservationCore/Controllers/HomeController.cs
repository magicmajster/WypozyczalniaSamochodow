using Microsoft.AspNetCore.Mvc;

namespace CarReservationCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
