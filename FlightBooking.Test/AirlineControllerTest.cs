using AutoFixture;
using AutoMapper;
using Azure.Core;
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
using Microsoft.OpenApi.Any;
using Moq;
using Xunit;

namespace FlightBooking.Test
{
    public class AirlineControllerTest
    {
        private readonly AirlineController _airlineController;

        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IAirlineService> _mockAirlineService;
        //private readonly Mock<GetAirlineByIdQuery> _mockGetAirlineByIdQuery;

        private List<AirlineDto> _airlinesDtoListFixture;
        private AirlineDto _airlineDtoFixture;

        public AirlineControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new AirlineMapper()));
            _mapper = mappingConfig.CreateMapper();

            _airlinesDtoListFixture = _fixture.CreateMany<AirlineDto>(3).ToList();
            _airlineDtoFixture = _fixture.Create<AirlineDto>();

            _mockMediator = new Mock<IMediator>();
            _mockAirlineService = new Mock<IAirlineService>();
            //_mockGetAirlineByIdQuery = new Mock<GetAirlineByIdQuery>();

            _airlineController = new AirlineController(_mockMediator.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightTypes()
        {
            // Arrange
            _mockMediator.SetReturnsDefault(_airlinesDtoListFixture);

            // Act
            var result = (ObjectResult)await _airlineController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<AirlineResponse>>();
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
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_Ok()
        {
            // Act
            var result = (ObjectResult)await _airlineController.GetAllAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<List<AirlineResponse>>();
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType()
        {
            // Arrange
            var expectedResponse = _mapper.Map<AirlineResponse>(_airlineDtoFixture);

            _mockMediator.Setup(m => m.Send(It.IsAny<AirlineDto>(), 
                                            It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockMediator.Object.Send(_airlineDtoFixture);

            // Assert
            result.Should().BeOfType<AirlineResponse>();
            result.Should().BeEquivalentTo(expectedResponse);

            _mockMediator.Verify(x => x.Send(_airlineDtoFixture, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_Ok()
        {
            // Act
            var result = (ObjectResult)await _airlineController.GetByIdAsync(It.IsAny<Guid>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType()
        {
            // Arrange
            var expectedResponse = _mapper.Map<AirlineResponse>(_airlineDtoFixture);

            _mockMediator.Setup(m => m.Send(It.IsAny<AirlineDto>(),
                                            It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _mockMediator.Object.Send(_airlineDtoFixture);

            // Assert
        }*/
    }
}
