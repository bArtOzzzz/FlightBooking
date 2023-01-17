using AutoFixture;
using AutoMapper;
using FlightBooking.API.Controllers;
using FlightBooking.API.Models.Response;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.CQRS.Airlines.QueryHandlers;
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
        private readonly Mock<IAirlineService> _mockAirlineService;

        private readonly Fixture _fixture = new();
        private readonly AirlineController _airlineController;

        private readonly IMapper _mapper;

        private List<AirlineDto> _airlinesDtoListFixture;

        public AirlineControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new AirlineMapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();
            _mockAirlineService = new Mock<IAirlineService>();

            _airlinesDtoListFixture = _fixture.CreateMany<AirlineDto>(3).ToList();

            _airlineController = new AirlineController(_mockMediator.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_Ok()
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

        /*[Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightTypes()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<AirlineResponse>>(_airlinesDtoListFixture);

            _mockMediator.Setup(m => m.Send(It.IsAny<List<AirlineDto>>(),
                                            It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockMediator.Object.Send(_airlinesDtoListFixture);

            // Assert
            result.Should().BeOfType<List<AirlineResponse>>();
            result.Should().BeEquivalentTo(expectedResponse);

            _mockMediator.Verify(x => x.Send(_airlinesDtoListFixture, It.IsAny<CancellationToken>()), Times.Once);
        }*/
    }
}
