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

            // Check if the email matches the CUET format
            if (!Regex.IsMatch(Email, emailPattern))
            {
                ModelState.AddModelError("Email", "Please enter a valid CUET student email (e.g., u1234567@student.cuet.ac.bd).");
                return View(); // Return back to the Signup form with the error message
            }

            // Here you would save the data (e.g., in a database)
            // For now, let's just log the data for demonstration
            _logger.LogInformation($"Name: {Name}, Email: {Email}, Password: {Password}, StudentId: {StudentId}");

            // Redirect to Login page after successful signup
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
