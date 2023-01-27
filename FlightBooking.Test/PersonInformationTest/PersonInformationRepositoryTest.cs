using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Repository;
using FlightBooking.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Common;
using Xunit;
using Microsoft.Extensions.Configuration;
using Moq.Dapper;
using Dapper;
using FluentAssertions;

namespace FlightBooking.Test.PersonInformationTest
{
    public class PersonInformationRepositoryTest
    {
        private PersonInformationRepository _personInformationRepository;

        private readonly Mock<IConfiguration> _mockConfiguration;

        public PersonInformationRepositoryTest()
        {
            _mockConfiguration = new Mock<IConfiguration>();

            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("FlightBookingDb");

            var context = new ApplicationDbContext(optionBuilder.Options);
            _personInformationRepository = new PersonInformationRepository(context, _mockConfiguration.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<PersonInformationEntity>() {
                new PersonInformationEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Citizenship = "Poland",
                    IdentificationNumber = "2763984J836PB3",
                    ExpirePasportDate = new DateTime(2025, 11, 13),
                    Name = "Мария",
                    Surname = "Лебедева",
                    BirthDate = new DateTime(1998, 8, 3),
                    Gender = "Ж",
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new PersonInformationEntity
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Citizenship = "America",
                    IdentificationNumber = "2763984J836PB3",
                    ExpirePasportDate = new DateTime(2024, 1, 15),
                    Name = "Владислав",
                    Surname = "Лазарев",
                    BirthDate = new DateTime(1993, 6, 11),
                    Gender = "М",
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },
            };

            connection.SetupDapperAsync(c => c.QueryAsync<PersonInformationEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<PersonInformationEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<PersonInformationEntity>>();
            result.Should().HaveCount(2);
            result[0].Should().BeEquivalentTo(expected[0]);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult()
        {
            var connection = new Mock<DbConnection>();

            PersonInformationEntity expected = new PersonInformationEntity();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<PersonInformationEntity>();
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var personInformationEntity = new PersonInformationEntity()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Citizenship = "America",
                IdentificationNumber = "2763984J836PB3",
                ExpirePasportDate = new DateTime(2024, 1, 15),
                Name = "Владислав",
                Surname = "Лазарев",
                BirthDate = new DateTime(1993, 6, 11),
                Gender = "М",
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _personInformationRepository.CreateAsync(personInformationEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var personInformationEntity = new PersonInformationEntity()
            {
                Id = new Guid("b568d785-5542-406e-81ad-63c38b7e01f5"),
                UserId = Guid.NewGuid(),
                Citizenship = "America",
                IdentificationNumber = "2763984J836PB3",
                ExpirePasportDate = new DateTime(2024, 1, 15),
                Name = "Владислав",
                Surname = "Лазарев",
                BirthDate = new DateTime(1993, 6, 11),
                Gender = "М",
                CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                CreatedTime = DateTime.Now.ToString("HH:m:s")
            };

            // Act
            var result = await _personInformationRepository.UpdateAsync(personInformationEntity.Id, personInformationEntity);

            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_CompletedResult()
        {
            // Act
            var result = await _personInformationRepository.DeleteAsync(new Guid("1da87518-fb76-421b-9d01-01e8fa5a2af4"));

            // Assert
            result.Should().BeTrue();
        }
    }
}
