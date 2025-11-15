using Cuet_Bus_Booking_System.Models;

namespace Cuet_Bus_Booking_System.Repository
{
    public interface IAuthRepository
    {
        Task<int> CreateAsync(User user);
        Task<User> LoginAsync(User user);
    }
}
