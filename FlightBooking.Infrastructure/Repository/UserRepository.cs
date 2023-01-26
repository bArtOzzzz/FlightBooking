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
            var sql = "SELECT * FROM Users";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<UsersEntity>(sql);
            return result.ToList();
        }

        public async Task<UsersEntity?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<UsersEntity>(sql, new { Id = id });
            return result;
        }
    }
}
