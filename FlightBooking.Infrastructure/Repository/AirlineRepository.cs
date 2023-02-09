using FlightBooking.Application.Abstractions.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FlightBooking.Infrastructure.Repository
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AirlineRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<AirlineEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM Airlines";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<AirlineEntity>(sql);
            await connection.CloseAsync();

            return result.ToList();
        }

        public async Task<AirlineEntity?> GetByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM Airlines WHERE Id = @Id";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<AirlineEntity>(sql, new { Id = id });
            await connection.CloseAsync();

            return result;
        }

        public async Task<Guid> CreateAsync(AirlineEntity airline)
        {
            AirlineEntity airlineEntity = new()
            {
                Id = Guid.NewGuid(),
                AirlineName = airline.AirlineName,
                Rating = airline.Rating,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            await _context.AddAsync(airlineEntity);
            await _context.SaveChangesAsync();

            return airlineEntity.Id;
        }

        public async Task<Guid> UpdateAsync(Guid id, AirlineEntity airline)
        {
            var currentAirline = await _context.Airlines.Where(a => a.Id.Equals(id))
                                                        .FirstOrDefaultAsync();

            currentAirline!.AirlineName = airline.AirlineName;
            currentAirline.Rating = airline.Rating;

            _context.Update(currentAirline);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currentAirline = await _context.Airlines.Where(a => a.Id.Equals(id))
                                                        .FirstOrDefaultAsync();

            if(currentAirline == null)
                return false;

            _context.Remove(currentAirline);
            var saved = _context.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
