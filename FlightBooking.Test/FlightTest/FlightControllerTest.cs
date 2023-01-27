using AutoFixture;
using AutoMapper;
using FlightBooking.API.Controllers;
using FlightBooking.API.Models.Request;
using FlightBooking.API.Models.Response;
using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FlightBooking.Test.FlightTest
{
    public class FlightControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly FlightController _flightController;

        private readonly Mock<IValidator<FlightCreateOrUpdateRequest>> _mockCreateOrUpdate;
        private readonly Mock<IValidator<FlightUpdateDateInformationRequest>> _mockUpdateInformation;
        private readonly Mock<IValidator<FlightUpdateDescriptionRequest>> _mockUpdateDescription;

        private readonly IMapper _mapper;

        private List<FlightDto> _flightDtoListFixture;
        private readonly FlightDto _flightDtoFixture;
        private readonly FlightCreateOrUpdateRequest _flightCreateOrUpdateFixture;

        public FlightControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();
            _mockCreateOrUpdate = new Mock<IValidator<FlightCreateOrUpdateRequest>>();
            _mockUpdateInformation = new Mock<IValidator<FlightUpdateDateInformationRequest>>();
            _mockUpdateDescription = new Mock<IValidator<FlightUpdateDescriptionRequest>>();

            _flightDtoListFixture = _fixture.CreateMany<FlightDto>(3).ToList();
            _flightDtoFixture = _fixture.Create<FlightDto>();
            _flightCreateOrUpdateFixture = _fixture.Create<FlightCreateOrUpdateRequest>();

            _flightController = new FlightController(_mockMediator.Object, _mapper, 
                                                     _mockCreateOrUpdate.Object, 
                                                     _mockUpdateDescription.Object, 
                                                     _mockUpdateInformation.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<FlightResponse>>(_flightDtoListFixture);

            _mockMediator.Setup(m => m.Send(new FlightGetAllQuery(), default))
                         .ReturnsAsync(_flightDtoListFixture);

            // Act
            var result = (ObjectResult)await _flightController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<FlightResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _flightDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new FlightGetAllQuery(), default))
                         .ReturnsAsync(_flightDtoListFixture);

            // Act
            var result = await _flightController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightGetAllQuery(), default))
                         .ReturnsAsync(new List<FlightDto>());

            // Act
            var result = await _flightController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<FlightResponse>(_flightDtoFixture);

            _mockMediator.Setup(m => m.Send(new FlightGetByIdQuery(_flightDtoFixture.Id), default))
                         .ReturnsAsync(_flightDtoFixture);

            // Act
            var result = (ObjectResult)await _flightController.GetByIdAsync(_flightDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<FlightResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _flightController.GetByIdAsync(_flightDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightGetByIdQuery(_flightDtoFixture.Id), default))
                         .ReturnsAsync(_flightDtoFixture);

            // Act
            var result = await _flightController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightCreateCommand(_flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightCreateOrUpdateRequest flightCreateOrUpdateRequest = new()
            {
                AirlineId = Guid.NewGuid(),
                AirplaneId = Guid.NewGuid(),
                Departurer = "Pekin",
                Arrival = "Paris",
                DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0).ToString(),
                ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0).ToString()
            };

            // Act
            var result = (ObjectResult)await _flightController.CreateAsync(flightCreateOrUpdateRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightCreateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _flightController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightCreateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightCreateOrUpdateRequest flightCreateOrUpdateRequest = new()
            {
                AirlineId = Guid.NewGuid(),
                AirplaneId = Guid.NewGuid(),
                Departurer = "Pekin",
                Arrival = "Paris",
                DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0).ToString(),
                ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0).ToString()
            };

            // Act
            var result = (ObjectResult)await _flightController.UpdateAsync(_flightDtoFixture.Id, flightCreateOrUpdateRequest);

            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateInformationDateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDateInformationCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightUpdateDateInformationRequest flightUpdateDateInformationRequest = new()
            {
                DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0).ToString(),
                ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0).ToString()
            };

            // Act
            var result = (ObjectResult)await _flightController.UpdateDateInformationAsync(_flightDtoFixture.Id, flightUpdateDateInformationRequest);

            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDateInformationCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateInformationDateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDateInformationCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightUpdateDateInformationRequest flightUpdateDateInformationRequest = new()
            {
                DepartureDate = new DateTime(2023, 5, 13, 10, 30, 0).ToString(),
                ArrivingDate = new DateTime(2023, 5, 13, 18, 55, 0).ToString()
            };

            // Act
            var result = await _flightController.UpdateDateInformationAsync(Guid.Empty, flightUpdateDateInformationRequest);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDateInformationCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateInformationDateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDateInformationCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            // Act
            var result = await _flightController.UpdateDateInformationAsync(_flightDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDateInformationCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateDescriptionAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDescriptionCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightUpdateDescriptionRequest flightUpdateDescription = new()
            {
                Departurer = "Pekin",
                Arrival = "Paris"
            };

            // Act
            var result = (ObjectResult)await _flightController.UpdateDescriptionAsync(_flightDtoFixture.Id, flightUpdateDescription);

            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDescriptionCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateDescriptionAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDescriptionCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            FlightUpdateDescriptionRequest flightUpdateDescription = new()
            {
                Departurer = "Pekin",
                Arrival = "Paris"
            };

            // Act
            var result = await _flightController.UpdateDescriptionAsync(Guid.Empty, flightUpdateDescription);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDescriptionCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateDescriptionAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightUpdateDescriptionCommand(_flightDtoFixture.Id, _flightDtoFixture), default))
                         .ReturnsAsync(_flightDtoFixture.Id);

            // Act
            var result = await _flightController.UpdateDescriptionAsync(_flightDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateDescriptionCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _flightController.UpdateAsync(Guid.Empty, _flightCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _flightController.UpdateAsync(_flightDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _flightController.DeleteAsync(_flightDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightDeleteCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _flightDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new FlightDeleteCommand(_flightDtoFixture.Id), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _flightController.DeleteAsync(_flightDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<FlightDeleteCommand>(), default), Times.Never);
        }
    }
}
