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

namespace FlightBooking.Test.PersonInformationTest
{
    public class PersonInformationHandlerTest
    {
        private readonly Mock<IPersonInformationService> _mockPersonInformationService;

        private readonly Fixture _fixture = new();

        private List<PersonInformationDto> _personInformationDtosListFixture;
        private PersonInformationDto _personInformationDtoFixture;

        public PersonInformationHandlerTest()
        {
            _mockPersonInformationService = new Mock<IPersonInformationService>();

            _personInformationDtosListFixture = _fixture.CreateMany<PersonInformationDto>(3).ToList();
            _personInformationDtoFixture = _fixture.Create<PersonInformationDto>();
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_CompletedResult_With_RightTypes()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.GetAllAsync())
                                         .ReturnsAsync(_personInformationDtosListFixture);

            var query = new PersonInformationGetAllQuery();
            var handler = new PersonInformationGetAllQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<List<PersonInformationDto>>();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(_personInformationDtosListFixture);

            _mockPersonInformationService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_NullCollection()
        {
            // Arrange
            _personInformationDtosListFixture = null!;

            _mockPersonInformationService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(_personInformationDtosListFixture);

            var query = new PersonInformationGetAllQuery();
            var handler = new PersonInformationGetAllQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull(null);

            _mockPersonInformationService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_Should_Return_FailedResult_With_EmptyCollection()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.GetAllAsync())
                               .ReturnsAsync(new List<PersonInformationDto>());

            var query = new PersonInformationGetAllQuery();
            var handler = new PersonInformationGetAllQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeEmpty();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_personInformationDtosListFixture);
            result.Should().BeOfType<List<PersonInformationDto>>();

            _mockPersonInformationService.Verify(a => a.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(_personInformationDtosListFixture[0]);

            var query = new PersonInformationGetByIdQuery(_personInformationDtosListFixture[0].Id);
            var handler = new PersonInformationGetByIdQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<PersonInformationDto>();
            result.Should().BeEquivalentTo(_personInformationDtosListFixture[0]);

            _mockPersonInformationService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_Null()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()));

            var query = new PersonInformationGetByIdQuery(_personInformationDtoFixture.Id);
            var handler = new PersonInformationGetByIdQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeNull();

            _mockPersonInformationService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_Should_Return_FailedResult_With_EmptyData()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(new PersonInformationDto());

            var query = new PersonInformationGetByIdQuery(_personInformationDtoFixture.Id);
            var handler = new PersonInformationGetByIdQueryHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            result.Should().BeOfType<PersonInformationDto>();
            result.Should().NotBeNull();
            result.Should().NotBeEquivalentTo(_personInformationDtoFixture);

            _mockPersonInformationService.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.CreateAsync(It.IsAny<PersonInformationDto>()))
                               .ReturnsAsync(new Guid[] { _personInformationDtoFixture.Id, _personInformationDtoFixture.UserId });

            var command = new PersonInformationCreateCommand(_personInformationDtoFixture);
            var handler = new PersoneInformationCreateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result[0].Should().NotBeEmpty();
            result[0].Should().Be(_personInformationDtoFixture.Id);
            result[1].Should().NotBeEmpty();
            result[1].Should().Be(_personInformationDtoFixture.UserId);

            _mockPersonInformationService.Verify(a => a.CreateAsync(It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _personInformationDtoFixture = null!;

            _mockPersonInformationService.Setup(config => config.CreateAsync(It.IsAny<PersonInformationDto>()));

            var command = new PersonInformationCreateCommand(_personInformationDtoFixture);
            var handler = new PersoneInformationCreateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockPersonInformationService.Verify(a => a.CreateAsync(It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            // Arrange
            PersonInformationDto airlineDto = new PersonInformationDto();

            _mockPersonInformationService.Setup(config => config.CreateAsync(It.IsAny<PersonInformationDto>()))
                               .ReturnsAsync(new Guid[] { airlineDto.Id, airlineDto.UserId });

            var command = new PersonInformationCreateCommand(_personInformationDtoFixture);
            var handler = new PersoneInformationCreateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            result[0].Should().BeEmpty();
            result[1].Should().BeEmpty();

            _mockPersonInformationService.Verify(a => a.CreateAsync(It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()))
                               .ReturnsAsync(_personInformationDtoFixture.Id);

            var command = new PersonInformationUpdateCommand(_personInformationDtoFixture.Id, _personInformationDtoFixture);
            var handler = new PersonInformationUpdateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();

            _mockPersonInformationService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsNullData()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()));

            var command = new PersonInformationUpdateCommand(_personInformationDtoFixture.Id, _personInformationDtoFixture);
            var handler = new PersonInformationUpdateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockPersonInformationService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_Should_Return_FaildResult_When_GetsEmptyData()
        {
            _personInformationDtoFixture = new PersonInformationDto();

            // Arrange
            _mockPersonInformationService.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()))
                                         .ReturnsAsync(_personInformationDtoFixture.Id);

            var command = new PersonInformationUpdateCommand(_personInformationDtoFixture.Id, _personInformationDtoFixture);
            var handler = new PersonInformationUpdateCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeEmpty();

            _mockPersonInformationService.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationDto>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_CompletedResult_With_RightType()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(true);

            var command = new PersonInformationDeleteCommand(_personInformationDtoFixture.Id);
            var handler = new PersonInformationDeleteCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeTrue();

            _mockPersonInformationService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FailedResult_When_False()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(false);

            var command = new PersonInformationDeleteCommand(_personInformationDtoFixture.Id);
            var handler = new PersonInformationDeleteCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockPersonInformationService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_Should_Return_FaildResult_When_NoReturnedData()
        {
            // Arrange
            _mockPersonInformationService.Setup(config => config.DeleteAsync(It.IsAny<Guid>()));

            var command = new PersonInformationDeleteCommand(_personInformationDtoFixture.Id);
            var handler = new PersonInformationDeleteCommandHandler(_mockPersonInformationService.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            result.Should().BeFalse();

            _mockPersonInformationService.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
