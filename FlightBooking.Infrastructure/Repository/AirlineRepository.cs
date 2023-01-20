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

        /// <summary>
        /// Gets all notes about airlines from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<AirlineEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Airlines";

            using (var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<AirlineEntity>(sql);
                return result.ToList();
            }      
        }

        /// <summary>
        /// Gets airline by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AirlineEntity?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Airlines WHERE Id = @Id";

            using (var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]))
            {
                await connection.OpenAsync();
                var result = await connection.QuerySingleOrDefaultAsync<AirlineEntity>(sql, new {  Id = id });
                return result;
            }
        }

        /// <summary>
        /// Creates new note about airline and saves in database
        /// </summary>
        /// <param name="airline"></param>
        /// <returns></returns>
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

            airline.Id = airlineEntity.Id;

            return airlineEntity.Id;
        }

        /// <summary>
        /// Updates information about existed airline by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airline"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes all information about existed airline from database and saves changes
        /// </summary>
        /// <param name="airlineEntity"></param>
        /// <returns></returns>
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
