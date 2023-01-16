﻿using AutoFixture;
using AutoMapper;
using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Dto;
using FlightBooking.Application.Mapper;
using FlightBooking.Application.Services;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlightBooking.Test
{
    public class AirlineServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IAirlineRepository> _mockAirlineRepository;

        private readonly AirlineService _airlineService;

        private List<AirlineEntity> _airlineEntitieListFixture;
        private AirlineEntity _airlineEntityFixture;

        public AirlineServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new AirlineMapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _airlineEntitieListFixture = _fixture.CreateMany<AirlineEntity>(2).ToList();
            _airlineEntityFixture = _fixture.Create<AirlineEntity>();

            _mockAirlineRepository = new Mock<IAirlineRepository>();

            _airlineService = new AirlineService(_mockAirlineRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.GetAllAsync())
                                  .ReturnsAsync(_airlineEntitieListFixture);

            var expectation = _mapper.Map<List<AirlineDto>>(_airlineEntitieListFixture);

            // Act
            var result = await _airlineService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<AirlineDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectation);

            _mockAirlineRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(_airlineEntitieListFixture[0]);

            var expectation = _mapper.Map<AirlineDto>(_airlineEntitieListFixture[0]);

            // Act
            var result = await _airlineService.GetByIdAsync(_airlineEntityFixture.Id);

            // Assert
            result.Should().BeOfType<AirlineDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockAirlineRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Return_Null()
        {
            // Arrange
            _airlineEntityFixture = null!;

            _mockAirlineRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(_airlineEntityFixture);

            // Act
            var result = await _airlineService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockAirlineRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
