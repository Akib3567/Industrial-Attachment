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
        private readonly IAuthRepository _authRepository;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthRepository userRepository)
        {
            _logger = logger;
            _authRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {
            if (user != null)
            {
                _authRepository.CreateAsync(user);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(User user)
        {
            // Authenticate the user and fetch their role from the database
            var result = await _authRepository.LoginAsync(user);

            if (result != null)
            {
                if (result.Role?.ToUpper() == "ADMIN")
                {
                    return RedirectToAction("Adminpage", "Bus");
                }
                else if (result.Role?.ToUpper() == "STUDENT")
                {
                    return RedirectToAction("userIndex", "User");
                }
            }

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
