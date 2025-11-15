using Cuet_Bus_Booking_System.Models;

namespace Cuet_Bus_Booking_System.Repository
{
    public interface IUserRepository
    {
        Task<int> BookSeat(BookingRequest booking);
        Task<IEnumerable<BookingDetails>> GetUserBookings(int userId);
        Task<IEnumerable<int>> GetBookedSeats(int busId);
        Task<int> CancelBooking(int bookingId);
    }
}
