using System.Diagnostics;
using Cuet_Bus_Booking_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        public IActionResult userIndex()
        {
            return View();
        }
        public IActionResult seatBooking()
        {
            return View();
        }

        public IActionResult myBooking()
        {
            return View();
        }
    }
}
