﻿using FlightBooking.API.Models.Response;
using FlightBooking.Application.Mapper;
using FlightBooking.API.Models.Request;
using FlightBooking.Application.Dto;
using FlightBooking.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using FluentValidation;
using AutoFixture;
using AutoMapper;
using MediatR;
using Xunit;
using Moq;
using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;

namespace FlightBooking.Test.AirlineTests
{
    public class AirlineControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly AirlineController _airlineController;

        private readonly Mock<IValidator<AirlineCreateOrUpdateRequest>> _mockValidator;

        private readonly IMapper _mapper;

        private List<AirlineDto> _airlinesDtoListFixture;
        private readonly AirlineDto _airlineDtoFixture;
        private readonly AirlineCreateOrUpdateRequest _airlineCreateOrUpdateFixture;

        public AirlineControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();
            _mockValidator = new Mock<IValidator<AirlineCreateOrUpdateRequest>>();

            _airlinesDtoListFixture = _fixture.CreateMany<AirlineDto>(3).ToList();
            _airlineDtoFixture = _fixture.Create<AirlineDto>();
            _airlineCreateOrUpdateFixture = _fixture.Create<AirlineCreateOrUpdateRequest>();

            _airlineController = new AirlineController(_mockMediator.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<AirlineResponse>>(_airlinesDtoListFixture);

            _mockMediator.Setup(m => m.Send(new AirlineGetAllQuery(), default))
                         .ReturnsAsync(_airlinesDtoListFixture);

            // Act
            var result = (ObjectResult)await _airlineController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<AirlineResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _airlinesDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new AirlineGetAllQuery(), default))
                         .ReturnsAsync(_airlinesDtoListFixture);

            // Act
            var result = await _airlineController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineGetAllQuery(), default))
                         .ReturnsAsync(new List<AirlineDto>());

            // Act
            var result = await _airlineController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<AirlineResponse>(_airlineDtoFixture);

            _mockMediator.Setup(m => m.Send(new AirlineGetByIdQuery(_airlineDtoFixture.Id), default))
                         .ReturnsAsync(_airlineDtoFixture);

            // Act
            var result = (ObjectResult)await _airlineController.GetByIdAsync(_airlineDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<AirlineResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _airlineController.GetByIdAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineGetByIdQuery(_airlineDtoFixture.Id), default))
                         .ReturnsAsync(_airlineDtoFixture);

            // Act
            var result = await _airlineController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineCreateCommand(_airlineDtoFixture), default))
                         .ReturnsAsync(_airlineDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _airlineController.CreateAsync(_airlineCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineCreateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineCreateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineUpdateCommand(_airlineDtoFixture.Id, _airlineDtoFixture), default))
                         .ReturnsAsync(_airlineDtoFixture.Id);

            // Act
            var result = (ObjectResult)await _airlineController.UpdateAsync(_airlineDtoFixture.Id, _airlineCreateOrUpdateFixture);

            // Assert
            result.Value.Should().BeOfType<Guid>();
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineUpdateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.UpdateAsync(Guid.Empty, _airlineCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _airlineController.UpdateAsync(_airlineDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _airlineController.DeleteAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineDeleteCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _airlineDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new AirlineDeleteCommand(_airlineDtoFixture.Id), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _airlineController.DeleteAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<AirlineDeleteCommand>(), default), Times.Never);
        }
    }
}
