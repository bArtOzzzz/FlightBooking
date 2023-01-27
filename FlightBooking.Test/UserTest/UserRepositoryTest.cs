using FlightBooking.Domain.Entities;
using System.Data.Common;
using FluentAssertions;
using Moq.Dapper;
using Dapper;
using Xunit;
using Moq;

namespace FlightBooking.Test.UserTest
{
    public class UserRepositoryTest
    {
        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_CompletedResult()
        {
            // Arrange
            var connection = new Mock<DbConnection>();
            var expected = new List<UsersEntity>() {
                new UsersEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },

                new UsersEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.ToString("MM-dd-yy"),
                    CreatedTime = DateTime.Now.ToString("HH:m:s")
                },
            };

            connection.SetupDapperAsync(c => c.QueryAsync<UsersEntity>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            // Act
            var result = (await connection.Object.QueryAsync<UsersEntity>("", null, null, null, null)).ToList();

            // Assert
            result.Should().BeOfType<List<UsersEntity>>();
            result.Should().HaveCount(2);
            result[0].Should().BeEquivalentTo(expected[0]);
        }

        [Fact]
        private void GetByIdAsync_OnSuccess_Returns_CompletedResult()
        {
            var connection = new Mock<DbConnection>();

            UsersEntity expected = new UsersEntity();

            connection.SetupDapperAsync(c => c.ExecuteScalarAsync<object>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected);

            var result = connection.Object.ExecuteScalarAsync<object>("")
                                          .GetAwaiter()
                                          .GetResult();

            result.Should().BeOfType<UsersEntity>();
            result.Should().BeEquivalentTo(expected);
        }
    }
}
