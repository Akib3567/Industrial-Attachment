using Cuet_Bus_Booking_System.Data;
using Cuet_Bus_Booking_System.Models;
using Dapper;

namespace Cuet_Bus_Booking_System.Repository
{
    public class BusRepository : IBusRepository
    {
        private readonly IDbContext _dbContext;

        public BusRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddBus(CuetBus bus)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = @"INSERT INTO Buses (BusName, TotalSeats, ScheduleTime) 
                          VALUES (@BusName, @TotalSeats, @ScheduleTime);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
                return await connection.QuerySingleAsync<int>(sql, new
                {
                    bus.BusName,
                    bus.TotalSeats,
                    bus.ScheduleTime
                });
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> UpdateBus(CuetBus bus)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = @"UPDATE Buses 
                          SET BusName = @BusName, 
                              TotalSeats = @TotalSeats, 
                              ScheduleTime = @ScheduleTime 
                          WHERE BusId = @BusId";
                return await connection.ExecuteAsync(sql, new
                {
                    bus.BusId,
                    bus.BusName,
                    bus.TotalSeats,
                    bus.ScheduleTime
                });
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<int> DeleteBus(int busId)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = "DELETE FROM Buses WHERE BusId = @BusId";
                return await connection.ExecuteAsync(sql, new { BusId = busId });
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<CuetBus> GetBusById(int busId)
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = "SELECT * FROM Buses WHERE BusId = @BusId";
                return await connection.QuerySingleOrDefaultAsync<CuetBus>(sql, new { BusId = busId });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CuetBus>> GetAllBuses()
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = "SELECT * FROM Buses ORDER BY BusId DESC";
                return await connection.QueryAsync<CuetBus>(sql);
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<CuetBus>();
            }
        }

        public async Task<IEnumerable<BookingDetails>> GetAllBookings()
        {
            try
            {
                using var connection = _dbContext.CreateConnection();
                string sql = @"SELECT b.BookingId, b.UserId, b.BusId, bs.BusName, 
                              b.SeatNumber, bs.ScheduleTime, b.BookingDate
                              FROM Bookings b
                              INNER JOIN Buses bs ON b.BusId = bs.BusId
                              ORDER BY b.BookingDate DESC";
                return await connection.QueryAsync<BookingDetails>(sql);
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<BookingDetails>();
            }
        }
    }
}