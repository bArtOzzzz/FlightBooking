using FlightBooking.Application.Abstractions.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FlightBooking.Infrastructure.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public FlightRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<FlightEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM Flights";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<FlightEntity>(sql);
            return result.ToList();
        }

        public async Task<FlightEntity?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Flights WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<FlightEntity>(sql, new { Id = id });
            return result;
        }

        public async Task<Guid> CreateAsync(FlightEntity flight)
        {
            FlightEntity flightEntity = new()
            {
                Id = Guid.NewGuid(),
                AirlineId = flight.AirlineId,
                AirplaneId = flight.AirplaneId,
                Departurer = flight.Departurer,
                Arrival = flight.Arrival,
                DepartureDate = flight.DepartureDate,
                ArrivingDate = flight.ArrivingDate,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s") 
            };

            await _context.AddAsync(flightEntity);
            await _context.SaveChangesAsync();

            return flightEntity.Id;
        }

        public async Task<Guid> UpdateAsync(Guid id, FlightEntity flight)
        {
            var currentFlight = await _context.Flights.Where(a => a.Id.Equals(id))
                                                      .FirstOrDefaultAsync();

            currentFlight!.Departurer = flight.Departurer;
            currentFlight.Arrival = flight.Arrival;
            currentFlight.DepartureDate = flight.DepartureDate;
            currentFlight.ArrivingDate = flight.ArrivingDate;

            _context.Update(currentFlight);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> UpdateDescriptionAsync(Guid id, FlightEntity flight)
        {
            var currentFlight = await _context.Flights.Where(a => a.Id.Equals(id))
                                                      .FirstOrDefaultAsync();

            currentFlight!.Departurer = flight.Departurer;
            currentFlight.Arrival = flight.Arrival;

            _context.Update(currentFlight);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> UpdateDateInformationAsync(Guid id, FlightEntity flight)
        {
            var currentFlight = await _context.Flights.Where(a => a.Id.Equals(id))
                                                      .FirstOrDefaultAsync();

            currentFlight!.DepartureDate = flight.DepartureDate;
            currentFlight.ArrivingDate = flight.ArrivingDate;

            _context.Update(currentFlight);
            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currentFlight = await _context.Flights.Where(a => a.Id.Equals(id))
                                                      .FirstOrDefaultAsync();

            if (currentFlight == null)
                return false;

            _context.Remove(currentFlight);
            var saved = _context.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
