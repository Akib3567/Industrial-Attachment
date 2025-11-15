using System.Diagnostics;
using Cuet_Bus_Booking_System.Models;
using Cuet_Bus_Booking_System.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IBusRepository _busRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IBusRepository busRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _busRepository = busRepository;
        }

        public async Task<IActionResult> userIndex()
        {
            var buses = await _busRepository.GetAllBuses();
            return View(buses);
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat([FromBody] BookingRequest booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRepository.BookSeat(booking);
                    if (result > 0)
                    {
                        return Ok(new { success = true, message = "Seat booked successfully", bookingId = result });
                    }
                    return BadRequest(new { success = false, message = "Failed to book seat" });
                }
                return BadRequest(new { success = false, message = "Invalid data" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while booking seat");
                return StatusCode(500, new { success = false, message = "An error occurred while booking the seat" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            try
            {
                var bookings = await _userRepository.GetUserBookings(userId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching bookings");
                return StatusCode(500, new { success = false, message = "An error occurred while fetching bookings" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBookedSeats(int busId)
        {
            try
            {
                var bookedSeats = await _userRepository.GetBookedSeats(busId);
                return Ok(bookedSeats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching booked seats");
                return StatusCode(500, new { success = false, message = "An error occurred while fetching booked seats" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            try
            {
                var result = await _userRepository.CancelBooking(bookingId);
                if (result > 0)
                {
                    return Ok(new { success = true, message = "Booking cancelled successfully" });
                }
                return NotFound(new { success = false, message = "Booking not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cancelling booking");
                return StatusCode(500, new { success = false, message = "An error occurred while cancelling the booking" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBookings(int userId)
        {
            try
            {
                var bookings = await _userRepository.GetUserBookings(userId);
                return PartialView("_BookingListPartial", bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user bookings");
                return PartialView("_BookingListPartial", Enumerable.Empty<BookingDetails>());
            }
        }
    }
}