using Cuet_Bus_Booking_System.Data;
using Cuet_Bus_Booking_System.Models;
using Dapper;

namespace Cuet_Bus_Booking_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> BookSeat(BookingRequest booking)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = @"INSERT INTO Bookings (UserId, BusId, SeatNumber, BookingDate) 
                              VALUES (@UserId, @BusId, @SeatNumber, GETDATE());
                              SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.QuerySingleAsync<int>(sql, new
                {
                    booking.UserId,
                    booking.BusId,
                    booking.SeatNumber
                });
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<IEnumerable<BookingDetails>> GetUserBookings(int userId)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = @"SELECT b.BookingId, b.UserId, b.BusId, bs.BusName, 
                              b.SeatNumber, bs.ScheduleTime, b.BookingDate
                              FROM Bookings b
                              INNER JOIN Buses bs ON b.BusId = bs.BusId
                              WHERE b.UserId = @UserId
                              ORDER BY b.BookingDate DESC";
                return await connection.QueryAsync<BookingDetails>(sql, new { UserId = userId });
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<BookingDetails>();
            }
        }

        public async Task<IEnumerable<int>> GetBookedSeats(int busId)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = "SELECT SeatNumber FROM Bookings WHERE BusId = @BusId";
                return await connection.QueryAsync<int>(sql, new { BusId = busId });
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<int>();
            }
        }

        public async Task<int> CancelBooking(int bookingId)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = "DELETE FROM Bookings WHERE BookingId = @BookingId";
                return await connection.ExecuteAsync(sql, new { BookingId = bookingId });
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}