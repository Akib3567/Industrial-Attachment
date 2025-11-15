using System.Diagnostics;
using Cuet_Bus_Booking_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class BusController : Controller
    {
        private readonly ILogger<BusController> _logger;

        public BusController(ILogger<BusController> logger)
        {
            _logger = logger;
        }
        public IActionResult Adminpage()
        {
            return View();
        }
        public IActionResult manageBooking()
        {
            return View();
        }

        public IActionResult viewBooking()
        {
            return View();
        }

    }
}
