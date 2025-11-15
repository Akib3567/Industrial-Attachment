using Cuet_Bus_Booking_System.Data;
using Cuet_Bus_Booking_System.Models;
using Dapper;
using Microsoft.AspNetCore.Connections;

namespace Cuet_Bus_Booking_System.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbContext _dbContext;

        public AuthRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(User user)
        {
            using var connection = _dbContext.CreateConnection();
            string sql = @"INSERT INTO Users (StudentId, Name, Email, Password) 
                          VALUES (@StudentId, @Name, @Email, @Password);
                          SELECT CAST(SCOPE_IDENTITY() as int)";

            return await connection.QuerySingleAsync<int>(sql, new
            {
                user.StudentId,
                user.Name,
                user.Email,
                user.Password
            });
        }

        public async Task<User> LoginAsync(User user)
        {
            using var connection = _dbContext.CreateConnection();
            string sql = @"SELECT * FROM [Users] WHERE Email = @Email AND Password = @Password";

            var res =  await connection.QueryFirstOrDefaultAsync<User>(sql, new
            {
                user.Email,
                user.Password
            });

            return res;
        }
    }
}