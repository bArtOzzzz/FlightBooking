using FlightBooking.Application.Abstractions.IRepositories;
using Microsoft.Extensions.Configuration;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FlightBooking.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration) => _configuration = configuration;

        public async Task<List<UsersEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM Users";

            using SqlConnection connection = new    (_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<UsersEntity>(sql);
            await connection.CloseAsync();

            return result.ToList();
        }

        public async Task<UsersEntity?> GetByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Users WHERE Id = @Id";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<UsersEntity>(sql, new { Id = id });
            await connection.CloseAsync();

            return result;
        }
    }
}
