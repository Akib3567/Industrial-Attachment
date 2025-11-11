using System.Data;

namespace Cuet_Bus_Booking_System.Data
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
