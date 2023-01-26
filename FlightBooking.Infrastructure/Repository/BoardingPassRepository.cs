using FlightBooking.Application.Abstractions.IRepositories;
using Microsoft.Extensions.Configuration;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repository
{
    public class BoardingPassRepository : IBoardingPassRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public BoardingPassRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _applicationDbContext = context;
            _configuration = configuration;
        }

        public async Task<List<BoardingPassEntity>> GetAllAsync()
        {
            var sql = "SELECT * FROM BoardingPasses";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<BoardingPassEntity>(sql);
            return result.ToList();
        }

        public async Task<BoardingPassEntity?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM BoardingPasses WHERE Id = @Id";

            using var connection = new SqlConnection(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QuerySingleOrDefaultAsync<BoardingPassEntity>(sql, new { Id = id });
            return result;
        }

        public async Task<Guid> CreateAsync(BoardingPassEntity boardingPass)
        {
            BoardingPassEntity boardingPassEntity = new()
            {
                Id = Guid.NewGuid(),
                FlightId = boardingPass.FlightId,
                UserId = boardingPass.UserId,
                Prise = boardingPass.Prise,
                isExpired = false,
                BookingExpireDate = boardingPass.BookingExpireDate,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            await _applicationDbContext.AddAsync(boardingPassEntity);
            await _applicationDbContext.SaveChangesAsync();

            return boardingPassEntity.Id;
        }

        public async Task<Guid> UpdateAsync(Guid id, BoardingPassEntity boardingPass)
        {
            var currentBoardingPass = await _applicationDbContext.BoardingPasses.Where(a => a.Id.Equals(id))
                                                                                .FirstOrDefaultAsync();

            currentBoardingPass!.isExpired = boardingPass.isExpired;
            currentBoardingPass.BookingExpireDate = boardingPass.BookingExpireDate;

            _applicationDbContext.Update(currentBoardingPass);
            await _applicationDbContext.SaveChangesAsync();

            return id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currentBoardingPass = await _applicationDbContext.BoardingPasses.Where(a => a.Id.Equals(id))
                                                                                .FirstOrDefaultAsync();

            if (currentBoardingPass == null)
                return false;

            _applicationDbContext.Remove(currentBoardingPass);
            var saved = _applicationDbContext.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
