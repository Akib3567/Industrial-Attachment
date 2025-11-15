using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cuet_Bus_Booking_System.Models;
using Cuet_Bus_Booking_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        //// POST: /Authentication/Signup
        //[HttpPost]
        //public IActionResult Signup(string Name, string Email, string Password, string StudentId)
        //{
        //    string emailPattern = @"^u\d{7}@student\.cuet\.ac\.bd$";

        //    if (!Regex.IsMatch(Email, emailPattern))
        //    {
        //        ModelState.AddModelError("Email", "Please enter a valid CUET student email (e.g., u1234567@student.cuet.ac.bd).");
        //        return View(); 
        //    }

        //    _logger.LogInformation($"Name: {Name}, Email: {Email}, Password: {Password}, StudentId: {StudentId}");

        //    return RedirectToAction("Login");
        //}

        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (user != null)
            {
                _userRepository.CreateAsync(user);
            }
            return RedirectToAction("Login");
        }

        /*[HttpPost]
        public async Task<IActionResult> Authenticate(User user)
        {
            var result = await _userRepository.LoginAsync(user);

            if (result != null)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }*/
        [HttpPost]
        public async Task<IActionResult> Authenticate(User user)
        {
            // Authenticate the user and fetch their role from the database
            var result = await _userRepository.LoginAsync(user);

            if (result != null)
            {
                if (result.Role == "admin")
                {
                    return RedirectToAction("Signup", "Authentication");  // Modify to 'AdminPage' when ready
                }
                else if (result.Role == "STUDENT")
                {
                    return RedirectToAction("Signup", "Authentication");  // Modify to 'UserPage' when ready
                }
            }

            // If login fails
            ViewBag.ErrorMessage = "Invalid credentials!";
            return View("Login"); 
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
