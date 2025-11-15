using Cuet_Bus_Booking_System.Models;
using Cuet_Bus_Booking_System.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cuet_Bus_Booking_System.Controllers
{
    public class BusController : Controller
    {
        private readonly IBusRepository _busRepository;
        private readonly ILogger<BusController> _logger;

        public BusController(ILogger<BusController> logger, IBusRepository busRepository)
        {
            _logger = logger;
            _busRepository = busRepository;
        }

        public async Task<IActionResult> Adminpage()
        {
            var buses = await _busRepository.GetAllBuses();
            return View(buses);
        }

        public IActionResult manageBooking()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings = await _busRepository.GetAllBookings();
                return PartialView("_AdminBookingListPartial", bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all bookings");
                return PartialView("_AdminBookingListPartial", Enumerable.Empty<BookingDetails>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBus([FromBody] CuetBus cuetBus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _busRepository.AddBus(cuetBus);
                    return Ok(new { success = true, message = "Bus added successfully" });
                }
                return BadRequest(new { success = false, message = "Invalid data" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding bus");
                return StatusCode(500, new { success = false, message = "An error occurred while adding the bus" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBus([FromBody] CuetBus cuetBus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _busRepository.UpdateBus(cuetBus);
                    if (result > 0)
                    {
                        return Ok(new { success = true, message = "Bus updated successfully" });
                    }
                    return NotFound(new { success = false, message = "Bus not found" });
                }
                return BadRequest(new { success = false, message = "Invalid data" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating bus");
                return StatusCode(500, new { success = false, message = "An error occurred while updating the bus" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBus(int id)
        {
            try
            {
                var result = await _busRepository.DeleteBus(id);
                if (result > 0)
                {
                    return Ok(new { success = true, message = "Bus deleted successfully" });
                }
                return NotFound(new { success = false, message = "Bus not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting bus");
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the bus" });
            }
        }
    }
}