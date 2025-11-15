using Cuet_Bus_Booking_System.Models;

namespace Cuet_Bus_Booking_System.Repository
{
    public interface IBusRepository
    {
        Task<int> AddBus(CuetBus bus);
        Task<int> UpdateBus(CuetBus bus);
        Task<int> DeleteBus(int busId);
        Task<CuetBus> GetBusById(int busId);
        Task<IEnumerable<CuetBus>> GetAllBuses();
        Task<IEnumerable<BookingDetails>> GetAllBookings();
    }
}