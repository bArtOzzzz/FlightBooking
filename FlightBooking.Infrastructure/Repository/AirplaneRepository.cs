using FlightBooking.Application.Abstractions.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FlightBooking.Infrastructure.Repository
{
    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public AirplaneRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

        public async Task<List<AirplaneEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Airplanes";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<AirplaneEntity>(sql);
            return result.ToList();
        }

        public async Task<AirplaneEntity?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Airplanes WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<AirplaneEntity>(sql, new { Id = id });
            return result;
        }

        public async Task<Guid> CreateAsync(AirplaneEntity airplane)
        {
            AirplaneEntity airplaneEntity = new()
            {
                Id = Guid.NewGuid(),
                ModelName = airplane.ModelName,
                MaximumSeats = airplane.MaximumSeats,
                MaximumWeight = airplane.MaximumWeight,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            await _applicationDbContext.AddAsync(airplaneEntity);
            await _applicationDbContext.SaveChangesAsync();

            return airplaneEntity.Id;
        }

        public async Task<Guid> UpdateAsync(Guid id, AirplaneEntity airplane)
        {
            var currentAirplane = await _applicationDbContext.Airplanes.Where(a => a.Id.Equals(id))
                                                                       .FirstOrDefaultAsync();

            currentAirplane!.ModelName = airplane.ModelName;
            currentAirplane.MaximumSeats = airplane.MaximumSeats;
            currentAirplane.MaximumWeight = airplane.MaximumWeight;

            _applicationDbContext.Update(currentAirplane);
            await _applicationDbContext.SaveChangesAsync();

            return id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currentAirplane = await _applicationDbContext.Airplanes.Where(a => a.Id.Equals(id))
                                                                       .FirstOrDefaultAsync();

            if (currentAirplane == null)
                return false;

            _applicationDbContext.Remove(currentAirplane);
            var saved = _applicationDbContext.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
