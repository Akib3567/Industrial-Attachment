using System.Diagnostics;
using System.Text.RegularExpressions;
using Cuet_Bus_Booking_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Authentication/Signup
        [HttpPost]
        public IActionResult Signup(string Name, string Email, string Password, string StudentId)
        {
            string emailPattern = @"^u\d{7}@student\.cuet\.ac\.bd$";

            if (!Regex.IsMatch(Email, emailPattern))
            {
                ModelState.AddModelError("Email", "Please enter a valid CUET student email (e.g., u1234567@student.cuet.ac.bd).");
                return View(); 
            }

            _logger.LogInformation($"Name: {Name}, Email: {Email}, Password: {Password}, StudentId: {StudentId}");

            return RedirectToAction("Login");
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
