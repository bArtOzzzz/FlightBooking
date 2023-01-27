using AutoFixture;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.CommandHandlers;
using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.CQRS.QueryHandlers;
using FlightBooking.Application.Dto;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlightBooking.Test.AirplaneTest
{
    public class AirplaneHandlerTest
    {
        private readonly Mock<IAirplaneService> _mockAirplaneService;

        private readonly Fixture _fixture = new();

        private List<AirplaneDto> _airplaneDtosListFixture;
        private AirplaneDto _airlplaneDtoFixture;

        public AirplaneHandlerTest()
        {
            _mockAirplaneService = new Mock<IAirplaneService>();

            _airplaneDtosListFixture = _fixture.CreateMany<AirplaneDto>(3).ToList();
            _airlplaneDtoFixture = _fixture.Create<AirplaneDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_airplaneDtosListFixture);

            var query = new AirplaneGetAllQuery();
            var handler = new AirplaneGetAllQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<AirplaneDto>>();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(_airplaneDtosListFixture);

            _mockAirplaneService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _airplaneDtosListFixture = null!;

            _mockAirplaneService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_airplaneDtosListFixture);

            var query = new AirplaneGetAllQuery();
            var handler = new AirplaneGetAllQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockAirplaneService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<AirplaneDto>());

            var query = new AirplaneGetAllQuery();
            var handler = new AirplaneGetAllQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_airplaneDtosListFixture);
            result.Should().BeOfType<List<AirplaneDto>>();

            _mockAirplaneService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_airplaneDtosListFixture[0]);

            var query = new AirplaneGetByIdQuery(_airplaneDtosListFixture[0].Id);
            var handler = new AirplaneGetByIdQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<AirplaneDto>();
            result.Should().BeEquivalentTo(_airplaneDtosListFixture[0]);

            _mockAirplaneService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new AirplaneGetByIdQuery(_airlplaneDtoFixture.Id);
            var handler = new AirplaneGetByIdQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockAirplaneService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new AirplaneDto());

            var query = new AirplaneGetByIdQuery(_airlplaneDtoFixture.Id);
            var handler = new AirplaneGetByIdQueryHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<AirplaneDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_airlplaneDtoFixture);

            _mockAirplaneService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.CreateAsync(It.IsAny<AirplaneDto>()))
                               .ReturnsAsync(_airlplaneDtoFixture.Id);

            var command = new AirplaneCreateCommand(_airlplaneDtoFixture);
            var handler = new AirplaneCreateCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_airlplaneDtoFixture.Id);

            _mockAirplaneService.Verify(a => a.CreateAsync(It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _airlplaneDtoFixture = null!;

            _mockAirplaneService.Setup(config => config.CreateAsync(It.IsAny<AirplaneDto>()));

            var command = new AirplaneCreateCommand(_airlplaneDtoFixture);
            var handler = new AirplaneCreateCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirplaneService.Verify(a => a.CreateAsync(It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            // Arrange
            AirplaneDto airlineDto = new AirplaneDto();

            _mockAirplaneService.Setup(config => config.CreateAsync(It.IsAny<AirplaneDto>()))
                               .ReturnsAsync(airlineDto.Id);

            var command = new AirplaneCreateCommand(_airlplaneDtoFixture);
            var handler = new AirplaneCreateCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirplaneService.Verify(a => a.CreateAsync(It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()))
                               .ReturnsAsync(_airlplaneDtoFixture.Id);

            var command = new AirplaneUpdateCommand(_airlplaneDtoFixture.Id, _airlplaneDtoFixture);
            var handler = new AirplaneUpdateCommandHandler(_mockAirplaneService.Object);

            // Acta
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _mockAirplaneService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()));

            var command = new AirplaneUpdateCommand(_airlplaneDtoFixture.Id, _airlplaneDtoFixture);
            var handler = new AirplaneUpdateCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirplaneService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            _airlplaneDtoFixture = new AirplaneDto();

            // Arrange
            _mockAirplaneService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()))
                                .ReturnsAsync(_airlplaneDtoFixture.Id);

            var command = new AirplaneUpdateCommand(_airlplaneDtoFixture.Id, _airlplaneDtoFixture);
            var handler = new AirplaneUpdateCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockAirplaneService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneDto>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(true);

            var command = new AirplaneDeleteCommand(_airlplaneDtoFixture.Id);
            var handler = new AirplaneDeleteCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();

            _mockAirplaneService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FailedResult_When_False()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(false);

            var command = new AirplaneDeleteCommand(_airlplaneDtoFixture.Id);
            var handler = new AirplaneDeleteCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockAirplaneService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FaildResult_When_NoReturnedData()
        {
            // Arrange
            _mockAirplaneService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()));

            var command = new AirplaneDeleteCommand(_airlplaneDtoFixture.Id);
            var handler = new AirplaneDeleteCommandHandler(_mockAirplaneService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockAirplaneService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
