using AutoFixture;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.CQRS.QueryHandlers;
using FlightBooking.Application.Dto;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlightBooking.Test.UserTest
{
    public class UserHandlerTest
    {
        private readonly Mock<IUserService> _mockUserService;

        private readonly Fixture _fixture = new();

        private List<UserDto> _usersDtosListFixture;
        private UserDto _userDtoFixture;

        public UserHandlerTest()
        {
            _mockUserService = new Mock<IUserService>();

            _usersDtosListFixture = _fixture.CreateMany<UserDto>(3).ToList();
            _userDtoFixture = _fixture.Create<UserDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockUserService.Setup(config => config.GetAllAsync())
                            .ReturnsAsync(_usersDtosListFixture);

            var query = new UserGetAllQuery();
            var handler = new UserGetAllQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<UserDto>>();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(_usersDtosListFixture);

            _mockUserService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _usersDtosListFixture = null!;

            _mockUserService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_usersDtosListFixture);

            var query = new UserGetAllQuery();
            var handler = new UserGetAllQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockUserService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockUserService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<UserDto>());

            var query = new UserGetAllQuery();
            var handler = new UserGetAllQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_usersDtosListFixture);
            result.Should().BeOfType<List<UserDto>>();

            _mockUserService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockUserService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_usersDtosListFixture[0]);

            var query = new UserGetByIdQuery(_usersDtosListFixture[0].Id);
            var handler = new UserGetByIdQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<UserDto>();
            result.Should().BeEquivalentTo(_usersDtosListFixture[0]);

            _mockUserService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockUserService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new UserGetByIdQuery(_userDtoFixture.Id);
            var handler = new UserGetByIdQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockUserService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockUserService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new UserDto());

            var query = new UserGetByIdQuery(_userDtoFixture.Id);
            var handler = new UserGetByIdQueryHandler(_mockUserService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<UserDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_userDtoFixture);

            _mockUserService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
