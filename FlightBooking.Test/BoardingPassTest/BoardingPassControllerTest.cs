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

namespace FlightBooking.Test.BoardingPassTest
{
    public class BoardingPassControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly BoardingPassController _boardingPassController;

        private readonly Mock<IValidator<BoardingPassCreateRequest>> _mockBoardingPassCreateValidator;
        private readonly Mock<IValidator<BoardingPassUpdateRequest>> _mockBoardingPassUpdateValidator;

        private readonly IMapper _mapper;

        private List<BoardingPassDto> _boardingPassDtoListFixture;
        private readonly BoardingPassDto _boardingPassDtoFixture;
        private readonly BoardingPassCreateRequest _boardingPassCreateFixture;
        private readonly BoardingPassUpdateRequest _boardingPassUpdateFixture;

        public BoardingPassControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();
            _mockBoardingPassCreateValidator = new Mock<IValidator<BoardingPassCreateRequest>>();
            _mockBoardingPassUpdateValidator = new Mock<IValidator<BoardingPassUpdateRequest>>();

            _boardingPassDtoListFixture = _fixture.CreateMany<BoardingPassDto>(3).ToList();
            _boardingPassDtoFixture = _fixture.Create<BoardingPassDto>();
            _boardingPassCreateFixture = _fixture.Create<BoardingPassCreateRequest>();
            _boardingPassUpdateFixture = _fixture.Create<BoardingPassUpdateRequest>();

            _boardingPassController = new BoardingPassController(_mockMediator.Object, _mapper, 
                                                                 _mockBoardingPassCreateValidator.Object, 
                                                                 _mockBoardingPassUpdateValidator.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<BoardingPassResponse>>(_boardingPassDtoListFixture);

            _mockMediator.Setup(m => m.Send(new BoardingPassGetAllQuery(), default))
                         .ReturnsAsync(_boardingPassDtoListFixture);

            // Act
            var result = (ObjectResult)await _boardingPassController.GetAllAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<BoardingPassResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _boardingPassDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new BoardingPassGetAllQuery(), default))
                         .ReturnsAsync(_boardingPassDtoListFixture);

            // Act
            var result = await _boardingPassController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassGetAllQuery(), default))
                         .ReturnsAsync(new List<BoardingPassDto>());

            // Act
            var result = await _boardingPassController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<BoardingPassResponse>(_boardingPassDtoFixture);

            _mockMediator.Setup(m => m.Send(new BoardingPassGetByIdQuery(_boardingPassDtoFixture.Id), default))
                         .ReturnsAsync(_boardingPassDtoFixture);

            // Act
            var result = (ObjectResult)await _boardingPassController.GetByIdAsync(_boardingPassDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<BoardingPassResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _boardingPassController.GetByIdAsync(_boardingPassDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassGetByIdQuery(_boardingPassDtoFixture.Id), default))
                         .ReturnsAsync(_boardingPassDtoFixture);

            // Act
            var result = await _boardingPassController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassCreateCommand(_boardingPassDtoFixture), default))
                         .ReturnsAsync(_boardingPassDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _boardingPassController.CreateAsync(_boardingPassCreateFixture);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassCreateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _boardingPassController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassCreateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassUpdateCommand(_boardingPassDtoFixture.Id, _boardingPassDtoFixture), default))
                         .ReturnsAsync(_boardingPassDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _boardingPassController.UpdateAsync(_boardingPassDtoFixture.Id, _boardingPassUpdateFixture);


            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassUpdateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _boardingPassController.UpdateAsync(Guid.Empty, _boardingPassUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _boardingPassController.UpdateAsync(_boardingPassDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _boardingPassController.DeleteAsync(_boardingPassDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassDeleteCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _boardingPassDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new BoardingPassDeleteCommand(_boardingPassDtoFixture.Id), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _boardingPassController.DeleteAsync(_boardingPassDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<BoardingPassDeleteCommand>(), default), Times.Never);
        }
    }
}
