using System.Diagnostics;
using Cuet_Bus_Booking_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        
    }
}
