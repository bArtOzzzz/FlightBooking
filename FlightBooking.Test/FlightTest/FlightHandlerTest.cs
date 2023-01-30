using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.CommandHandlers;
using FlightBooking.Application.CQRS.QueryHandlers;
using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using FluentAssertions;
using AutoFixture;
using Xunit;
using Moq;

namespace FlightBooking.Test.FlightTest
{
    public class FlightHandlerTest
    {
        private readonly Mock<IFlightService> _mockFlightService;

        private readonly Fixture _fixture = new();

        private List<FlightDto> _flightDtosListFixture;
        private FlightDto _flightDtoFixture;

        public FlightHandlerTest()
        {
            _mockFlightService = new Mock<IFlightService>();

            _flightDtosListFixture = _fixture.CreateMany<FlightDto>(3).ToList();
            _flightDtoFixture = _fixture.Create<FlightDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockFlightService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_flightDtosListFixture);

            var query = new FlightGetAllQuery();
            var handler = new FlightGetAllQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<FlightDto>>();
            result.Should().BeEquivalentTo(_flightDtosListFixture);

            _mockFlightService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _flightDtosListFixture = null!;

            _mockFlightService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_flightDtosListFixture);

            var query = new FlightGetAllQuery();
            var handler = new FlightGetAllQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockFlightService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockFlightService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<FlightDto>());

            var query = new FlightGetAllQuery();
            var handler = new FlightGetAllQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<FlightDto>>();
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_flightDtosListFixture);

            _mockFlightService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockFlightService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_flightDtosListFixture[0]);

            var query = new FlightGetByIdQuery(_flightDtosListFixture[0].Id);
            var handler = new FlightGetByIdQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<FlightDto>();
            result.Should().BeEquivalentTo(_flightDtosListFixture[0]);

            _mockFlightService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockFlightService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new FlightGetByIdQuery(_flightDtoFixture.Id);
            var handler = new FlightGetByIdQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockFlightService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockFlightService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new FlightDto());

            var query = new FlightGetByIdQuery(_flightDtoFixture.Id);
            var handler = new FlightGetByIdQueryHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<FlightDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_flightDtoFixture);

            _mockFlightService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockFlightService.Setup(config => config.CreateAsync(It.IsAny<FlightDto>()))
                               .ReturnsAsync(_flightDtoFixture.Id);

            var command = new FlightCreateCommand(_flightDtoFixture);
            var handler = new FlightCreateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_flightDtoFixture.Id);

            _mockFlightService.Verify(a => a.CreateAsync(It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _flightDtoFixture = null!;

            _mockFlightService.Setup(config => config.CreateAsync(It.IsAny<FlightDto>()));

            var command = new FlightCreateCommand(_flightDtoFixture);
            var handler = new FlightCreateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockFlightService.Verify(a => a.CreateAsync(It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            // Arrange
            FlightDto airlineDto = new FlightDto();

            _mockFlightService.Setup(config => config.CreateAsync(It.IsAny<FlightDto>()))
                               .ReturnsAsync(airlineDto.Id);

            var command = new FlightCreateCommand(_flightDtoFixture);
            var handler = new FlightCreateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockFlightService.Verify(a => a.CreateAsync(It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockFlightService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()))
                              .ReturnsAsync(_flightDtoFixture.Id);

            var command = new FlightUpdateCommand(_flightDtoFixture.Id, _flightDtoFixture);
            var handler = new FlightUpdateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _mockFlightService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _mockFlightService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()));

            var command = new FlightUpdateCommand(_flightDtoFixture.Id, _flightDtoFixture);
            var handler = new FlightUpdateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockFlightService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            _flightDtoFixture = new FlightDto();

            // Arrange
            _mockFlightService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()))
                              .ReturnsAsync(_flightDtoFixture.Id);

            var command = new FlightUpdateCommand(_flightDtoFixture.Id, _flightDtoFixture);
            var handler = new FlightUpdateCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockFlightService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightDto>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockFlightService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(true);

            var command = new FlightDeleteCommand(_flightDtoFixture.Id);
            var handler = new FlightDeleteCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();

            _mockFlightService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FailedResult_When_False()
        {
            // Arrange
            _mockFlightService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(false);

            var command = new FlightDeleteCommand(_flightDtoFixture.Id);
            var handler = new FlightDeleteCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockFlightService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FaildResult_When_NoReturnedData()
        {
            // Arrange
            _mockFlightService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()));

            var command = new FlightDeleteCommand(_flightDtoFixture.Id);
            var handler = new FlightDeleteCommandHandler(_mockFlightService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockFlightService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
