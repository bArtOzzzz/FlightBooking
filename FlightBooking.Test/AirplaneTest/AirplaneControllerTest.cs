using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.API.Models.Response;
using FlightBooking.API.Models.Request;
using FlightBooking.API.Controllers;
using FlightBooking.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using FluentValidation;
using AutoFixture;
using AutoMapper;
using MediatR;
using Xunit;
using Moq;

namespace FlightBooking.Test.AirplaneTest
{
    public class AirplaneControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly AirplaneController _airplaneController;

        private readonly Mock<IValidator<AirplaneCreateOrUpdateRequest>> _mockValidator;

        private readonly IMapper _mapper;

        private List<AirplaneDto> _airplanesDtoListFixture;
        private readonly AirplaneDto _airplaneDtoFixture;
        private readonly AirplaneCreateOrUpdateRequest _airplaneCreateOrUpdateFixture;

        public AirplaneControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();
            _mockValidator = new Mock<IValidator<AirplaneCreateOrUpdateRequest>>();

            _airplanesDtoListFixture = _fixture.CreateMany<AirplaneDto>(3).ToList();
            _airplaneDtoFixture = _fixture.Create<AirplaneDto>();
            _airplaneCreateOrUpdateFixture = _fixture.Create<AirplaneCreateOrUpdateRequest>();

            _airplaneController = new AirplaneController(_mockMediator.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<AirplaneResponse>>(_airplanesDtoListFixture);

            _mockMediator.Setup(m => m.Send(new AirplaneGetAllQuery(), default))
                         .ReturnsAsync(_airplanesDtoListFixture);

            // Act
            var result = (ObjectResult)await _airplaneController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<AirplaneResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _airplanesDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new AirplaneGetAllQuery(), default))
                         .ReturnsAsync(_airplanesDtoListFixture);

            // Act
            var result = await _airplaneController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneGetAllQuery(), default))
                         .ReturnsAsync(new List<AirplaneDto>());

            // Act
            var result = await _airplaneController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<AirplaneResponse>(_airplaneDtoFixture);

            _mockMediator.Setup(m => m.Send(new AirplaneGetByIdQuery(_airplaneDtoFixture.Id), default))
                         .ReturnsAsync(_airplaneDtoFixture);

            // Act
            var result = (ObjectResult)await _airplaneController.GetByIdAsync(_airplaneDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<AirplaneResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _airplaneController.GetByIdAsync(_airplaneDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneGetByIdQuery(_airplaneDtoFixture.Id), default))
                         .ReturnsAsync(_airplaneDtoFixture);

            // Act
            var result = await _airplaneController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneCreateCommand(_airplaneDtoFixture), default))
                         .ReturnsAsync(_airplaneDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _airplaneController.CreateAsync(_airplaneCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneCreateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airplaneController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneCreateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneUpdateCommand(_airplaneDtoFixture.Id, _airplaneDtoFixture), default))
                         .ReturnsAsync(_airplaneDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _airplaneController.UpdateAsync(_airplaneDtoFixture.Id, _airplaneCreateOrUpdateFixture);

            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneUpdateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _airplaneController.UpdateAsync(Guid.Empty, _airplaneCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airplaneController.UpdateAsync(_airplaneDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _airplaneController.DeleteAsync(_airplaneDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneDeleteCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _airplaneDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new AirplaneDeleteCommand(_airplaneDtoFixture.Id), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _airplaneController.DeleteAsync(_airplaneDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirplaneDeleteCommand>(), default), Times.Never);
        }
    }
}
