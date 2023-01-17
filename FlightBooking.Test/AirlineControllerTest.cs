using AutoFixture;
using AutoMapper;
using FlightBooking.API.Controllers;
using FlightBooking.API.Models.Request;
using FlightBooking.API.Models.Response;
using FlightBooking.Application.CQRS.Airlines.Commands;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using FlightBooking.Application.Mapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FlightBooking.Test
{
    public class AirlineControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly AirlineController _airlineController;

        private readonly IMapper _mapper;

        private List<AirlineDto> _airlinesDtoListFixture;
        private readonly AirlineDto _airlineDtoFixture;
        private readonly AirlineCreateOrUpdate _airlineCreateOrUpdateFixture;

        public AirlineControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new AirlineMapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();

            _airlinesDtoListFixture = _fixture.CreateMany<AirlineDto>(3).ToList();
            _airlineDtoFixture = _fixture.Create<AirlineDto>();
            _airlineCreateOrUpdateFixture = _fixture.Create<AirlineCreateOrUpdate>();

            _airlineController = new AirlineController(_mockMediator.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<AirlineResponse>>(_airlinesDtoListFixture);

            _mockMediator.Setup(m => m.Send(new GetAllAirlinesQuery(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_airlinesDtoListFixture);

            // Act
            var result = (ObjectResult)await _airlineController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<AirlineResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllAirlinesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _airlinesDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new GetAllAirlinesQuery(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_airlinesDtoListFixture);

            // Act
            var result = await _airlineController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllAirlinesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new GetAllAirlinesQuery(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(new List<AirlineDto>());

            // Act
            var result = await _airlineController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllAirlinesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult() 
        {
            // Arrange
            var expectedResponse = _mapper.Map<AirlineResponse>(_airlineDtoFixture);

            _mockMediator.Setup(m => m.Send(new GetAirlineByIdQuery(_airlineDtoFixture.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_airlineDtoFixture);

            // Act
            var result = (ObjectResult)await _airlineController.GetByIdAsync(_airlineDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<AirlineResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAirlineByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new GetAirlineByIdQuery(Guid.Empty), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_airlineDtoFixture);

            // Act
            var result = await _airlineController.GetByIdAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAirlineByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenResponseIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new GetAirlineByIdQuery(_airlineDtoFixture.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(_airlineDtoFixture);

            // Act
            var result = await _airlineController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAirlineByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        // TODO: Impossible to get OkObjectResult => always NotFoundResult due to empty Guid value
        /*[Fact]
        private async Task CreateAsync_OnSuccess_Returns_OkResult()
        {
            var expectedResponse = _mapper.Map<AirlineDto>(_airlineCreateOrUpdateFixture);

           var resultMock = _mockMediator.Setup(m => m.Send(new CreateAirlineCommand(expectedResponse), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse.Id);

            // Act
            var result = await _airlineController.CreateAsync(_airlineCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }*/

        [Fact]
        private async Task CreateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.CreateAsync(_airlineCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.CreateAsync(new AirlineCreateOrUpdate());

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        // TODO: Impossible to get OkObjectResult => always NotFoundResult due to empty Guid value
        /*[Fact]
        private async Task UpdateAsync_OnSuccsess_Returns()
        {
            var newGuid = new Guid("bbb47534-699d-49a9-b8bb-812ebdae8391");

            // Arrange
            _mockMediator.Setup(m => m.Send(new UpdateAirlineCommand(_airlineDtoFixture.Id, _airlineDtoFixture), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(It.IsAny<Guid>());

            // Act
            var result = (ObjectResult)await _airlineController.UpdateAsync(_airlineDtoFixture.Id, _airlineCreateOrUpdateFixture);

            // Assert
            //result.Value.Should().BeOfType<AirlineResponse>();
            result.Value.Should().BeEquivalentTo(_airlineDtoFixture.Id);
            result.Should().BeOfType<OkObjectResult>();

           //_mockMediator.Verify(x => x.Send(It.IsAny<GetAirlineByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }*/

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            Guid id = Guid.Empty;

            // Act
            var result = await _airlineController.UpdateAsync(id, _airlineCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.UpdateAsync(_airlineDtoFixture.Id, new AirlineCreateOrUpdate());

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.UpdateAsync(_airlineDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _airlineController.DeleteAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _airlineDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new DeleteAirlineCommand(_airlineDtoFixture.Id), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(true);

            // Act
            var result = await _airlineController.DeleteAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteAirlineCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
