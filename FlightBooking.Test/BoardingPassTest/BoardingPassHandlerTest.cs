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

namespace FlightBooking.Test.BoardingPassTest
{
    public class BoardingPassHandlerTest
    {
        private readonly Mock<IBoardingPassService> _mockBoardingPassService;

        private readonly Fixture _fixture = new();

        private List<BoardingPassDto> _boardingPassDtosListFixture;
        private BoardingPassDto _boardingPassDtoFixture;

        public BoardingPassHandlerTest()
        {
            _mockBoardingPassService = new Mock<IBoardingPassService>();

            _boardingPassDtosListFixture = _fixture.CreateMany<BoardingPassDto>(3).ToList();
            _boardingPassDtoFixture = _fixture.Create<BoardingPassDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_boardingPassDtosListFixture);

            var query = new BoardingPassGetAllQuery();
            var handler = new BoardingPassGetAllQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<BoardingPassDto>>();
            result.Should().BeEquivalentTo(_boardingPassDtosListFixture);

            _mockBoardingPassService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _boardingPassDtosListFixture = null!;

            _mockBoardingPassService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_boardingPassDtosListFixture);

            var query = new BoardingPassGetAllQuery();
            var handler = new BoardingPassGetAllQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockBoardingPassService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<BoardingPassDto>());

            var query = new BoardingPassGetAllQuery();
            var handler = new BoardingPassGetAllQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<BoardingPassDto>>();
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_boardingPassDtosListFixture);

            _mockBoardingPassService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_boardingPassDtosListFixture[0]);

            var query = new BoardingPassGetByIdQuery(_boardingPassDtosListFixture[0].Id);
            var handler = new BoardingPassGetByIdQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<BoardingPassDto>();
            result.Should().BeEquivalentTo(_boardingPassDtosListFixture[0]);

            _mockBoardingPassService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new BoardingPassGetByIdQuery(_boardingPassDtoFixture.Id);
            var handler = new BoardingPassGetByIdQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockBoardingPassService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new BoardingPassDto());

            var query = new BoardingPassGetByIdQuery(_boardingPassDtoFixture.Id);
            var handler = new BoardingPassGetByIdQueryHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<BoardingPassDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_boardingPassDtoFixture);

            _mockBoardingPassService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.CreateAsync(It.IsAny<BoardingPassDto>()))
                               .ReturnsAsync(_boardingPassDtoFixture.Id);

            var command = new BoardingPassCreateCommand(_boardingPassDtoFixture);
            var handler = new BoardingPassCreateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_boardingPassDtoFixture.Id);

            _mockBoardingPassService.Verify(a => a.CreateAsync(It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _boardingPassDtoFixture = null!;

            _mockBoardingPassService.Setup(config => config.CreateAsync(It.IsAny<BoardingPassDto>()));

            var command = new BoardingPassCreateCommand(_boardingPassDtoFixture);
            var handler = new BoardingPassCreateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockBoardingPassService.Verify(a => a.CreateAsync(It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            // Arrange
            BoardingPassDto airlineDto = new BoardingPassDto();

            _mockBoardingPassService.Setup(config => config.CreateAsync(It.IsAny<BoardingPassDto>()))
                               .ReturnsAsync(airlineDto.Id);

            var command = new BoardingPassCreateCommand(_boardingPassDtoFixture);
            var handler = new BoardingPassCreateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockBoardingPassService.Verify(a => a.CreateAsync(It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()))
                               .ReturnsAsync(_boardingPassDtoFixture.Id);

            var command = new BoardingPassUpdateCommand(_boardingPassDtoFixture.Id, _boardingPassDtoFixture);
            var handler = new BoardingPassUpdateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _mockBoardingPassService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()));

            var command = new BoardingPassUpdateCommand(_boardingPassDtoFixture.Id, _boardingPassDtoFixture);
            var handler = new BoardingPassUpdateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockBoardingPassService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            _boardingPassDtoFixture = new BoardingPassDto();

            // Arrange
            _mockBoardingPassService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()))
                               .ReturnsAsync(_boardingPassDtoFixture.Id);

            var command = new BoardingPassUpdateCommand(_boardingPassDtoFixture.Id, _boardingPassDtoFixture);
            var handler = new BoardingPassUpdateCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockBoardingPassService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassDto>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(true);

            var command = new BoardingPassDeleteCommand(_boardingPassDtoFixture.Id);
            var handler = new BoardingPassDeleteCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();

            _mockBoardingPassService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FailedResult_When_False()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(false);

            var command = new BoardingPassDeleteCommand(_boardingPassDtoFixture.Id);
            var handler = new BoardingPassDeleteCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockBoardingPassService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FaildResult_When_NoReturnedData()
        {
            // Arrange
            _mockBoardingPassService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()));

            var command = new BoardingPassDeleteCommand(_boardingPassDtoFixture.Id);
            var handler = new BoardingPassDeleteCommandHandler(_mockBoardingPassService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockBoardingPassService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
