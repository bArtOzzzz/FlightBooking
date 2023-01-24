using FlightBooking.Application.CQRS.Airlines.CommandsHandler;
using FlightBooking.Application.CQRS.Airlines.QueryHandlers;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using FluentAssertions;
using AutoFixture;
using Xunit;
using Moq;

namespace FlightBooking.Test.AirlineTests
{
    public class AirlineHandlersTest
    {
        private readonly Mock<IAirlineService> _mockAirlineService;

        private readonly Fixture _fixture = new();

        private List<AirlineDto> _airlineDtosListFixture;
        private AirlineDto _airlineDtoFixture;

        public AirlineHandlersTest()
        {
            _mockAirlineService = new Mock<IAirlineService>();

            _airlineDtosListFixture = _fixture.CreateMany<AirlineDto>(3).ToList();
            _airlineDtoFixture = _fixture.Create<AirlineDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_airlineDtosListFixture);

            var query = new AirlineGetAllQuery();
            var handler = new AirlineGetAllQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<AirlineDto>>();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(_airlineDtosListFixture);

            _mockAirlineService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _airlineDtosListFixture = null!;

            _mockAirlineService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_airlineDtosListFixture);

            var query = new AirlineGetAllQuery();
            var handler = new AirlineGetAllQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockAirlineService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<AirlineDto>());

            var query = new AirlineGetAllQuery();
            var handler = new AirlineGetAllQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_airlineDtosListFixture);
            result.Should().BeOfType<List<AirlineDto>>();

            _mockAirlineService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_airlineDtosListFixture[0]);

            var query = new GetByIdAsyncQuery(_airlineDtosListFixture[0].Id);
            var handler = new AirlineGetByIdQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<AirlineDto>();
            result.Should().BeEquivalentTo(_airlineDtosListFixture[0]);

            _mockAirlineService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new GetByIdAsyncQuery(_airlineDtoFixture.Id);
            var handler = new AirlineGetByIdQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockAirlineService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new AirlineDto());

            var query = new GetByIdAsyncQuery(_airlineDtoFixture.Id);
            var handler = new AirlineGetByIdQueryHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<AirlineDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_airlineDtoFixture);

            _mockAirlineService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.CreateAsync(It.IsAny<AirlineDto>()))
                               .ReturnsAsync(_airlineDtoFixture.Id);

            var command = new AirlineCreateAsyncCommand(_airlineDtoFixture);
            var handler = new AirlineCreateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_airlineDtoFixture.Id);

            _mockAirlineService.Verify(a => a.CreateAsync(It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _airlineDtoFixture = null!;

            _mockAirlineService.Setup(config => config.CreateAsync(It.IsAny<AirlineDto>()));

            var command = new AirlineCreateAsyncCommand(_airlineDtoFixture);
            var handler = new AirlineCreateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirlineService.Verify(a => a.CreateAsync(It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            // Arrange
            AirlineDto airlineDto = new AirlineDto();

            _mockAirlineService.Setup(config => config.CreateAsync(It.IsAny<AirlineDto>()))
                               .ReturnsAsync(airlineDto.Id);

            var command = new AirlineCreateAsyncCommand(_airlineDtoFixture);
            var handler = new AirlineCreateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirlineService.Verify(a => a.CreateAsync(It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()))
                               .ReturnsAsync(_airlineDtoFixture.Id);

            var command = new UpdateAsyncCommand(_airlineDtoFixture.Id, _airlineDtoFixture);
            var handler = new AirlineUpdateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _mockAirlineService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()));

            var command = new UpdateAsyncCommand(_airlineDtoFixture.Id, _airlineDtoFixture);
            var handler = new AirlineUpdateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirlineService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            _airlineDtoFixture = new AirlineDto();

            // Arrange
            _mockAirlineService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()))
                               .ReturnsAsync(_airlineDtoFixture.Id);

            var command = new UpdateAsyncCommand(_airlineDtoFixture.Id, _airlineDtoFixture);
            var handler = new AirlineUpdateCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirlineService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineDto>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(true);

            var command = new DeleteAsyncCommand(_airlineDtoFixture.Id);
            var handler = new AirlineDeleteCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();

            _mockAirlineService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FailedResult_When_False()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(false);

            var command = new DeleteAsyncCommand(_airlineDtoFixture.Id);
            var handler = new AirlineDeleteCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockAirlineService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FaildResult_When_NoReturnedData()
        {
            // Arrange
            _mockAirlineService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()));

            var command = new DeleteAsyncCommand(_airlineDtoFixture.Id);
            var handler = new AirlineDeleteCommandHandler(_mockAirlineService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockAirlineService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
