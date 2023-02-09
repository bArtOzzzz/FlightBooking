using FlightBooking.Application.Abstractions.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using Microsoft.Data.SqlClient;
using Dapper;

namespace FlightBooking.Infrastructure.Repository
{
    public class PersonInformationRepository : IPersonInformationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;

        public PersonInformationRepository(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;
        }

        public async Task<List<PersonInformationEntity>> GetAllAsync()
        {
            string sql = "SELECT * FROM PersonInformations";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<PersonInformationEntity>(sql);
            await connection.CloseAsync();

            return result.ToList();
        }

        public async Task<PersonInformationEntity?> GetByIdAsync(Guid id)
        {
            string sql = "SELECT * FROM PersonInformations WHERE Id = @Id";

            using SqlConnection connection = new(_configuration["DefaultConnectionToLocalDatabase"]);
            await connection.OpenAsync();
            var result = await connection.QueryFirstOrDefaultAsync<PersonInformationEntity>(sql, new { Id = id });
            await connection.CloseAsync();

            return result;
        }

        public async Task<Guid[]> CreateAsync(PersonInformationEntity personInformation)
        {
            UsersEntity usersEntity = new()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            PersonInformationEntity personInformationEntity = new()
            {
                Id = Guid.NewGuid(),
                UserId = usersEntity.Id,
                Citizenship = personInformation.Citizenship,
                IdentificationNumber = personInformation.IdentificationNumber,
                ExpirePasportDate = personInformation.ExpirePasportDate,
                Name = personInformation.Name,
                Surname = personInformation.Surname,
                BirthDate = personInformation.BirthDate,
                Gender = personInformation.Gender,
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            await _applicationDbContext.AddRangeAsync(usersEntity, personInformationEntity);
            await _applicationDbContext.SaveChangesAsync();

            return new Guid[] { usersEntity.Id, personInformationEntity.Id };
        }

        public async Task<Guid> UpdateAsync(Guid id, PersonInformationEntity personInformation)
        {
            var currentPersonInformation = await _applicationDbContext.PersonInformations.Where(pi => pi.Id.Equals(id))
                                                                                         .FirstOrDefaultAsync();

            currentPersonInformation!.Citizenship = personInformation.Citizenship;
            currentPersonInformation.IdentificationNumber = personInformation.IdentificationNumber;
            currentPersonInformation.ExpirePasportDate = personInformation.ExpirePasportDate;
            currentPersonInformation.Name = personInformation.Name;
            currentPersonInformation.Surname = personInformation.Surname;
            currentPersonInformation.BirthDate = personInformation.BirthDate;
            currentPersonInformation.Gender = personInformation?.Gender;

            _applicationDbContext.Update(currentPersonInformation);
            await _applicationDbContext.SaveChangesAsync();

            return id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var currentPersonInformation = await _applicationDbContext.PersonInformations.Where(a => a.Id.Equals(id))
                                                                                         .FirstOrDefaultAsync();

            var currentUser = await _applicationDbContext.Users.Where(a => a.Id.Equals(currentPersonInformation!.UserId))
                                                               .FirstOrDefaultAsync();

            if (currentPersonInformation == null)
                return false;

            _applicationDbContext.RemoveRange(currentPersonInformation, currentUser!);
            var saved = _applicationDbContext.SaveChangesAsync();

            return await saved > 0;
        }
    }
}
