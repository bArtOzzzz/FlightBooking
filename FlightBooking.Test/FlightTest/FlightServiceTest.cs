using AutoFixture;
using AutoMapper;
using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Dto;
using FlightBooking.Application.Services;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlightBooking.Test.FlightTest
{
    public class FlightServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IFlightRepository> _mockFlightRepository;

        private readonly FlightService _flightService;

        private List<FlightEntity> _flightEntitieListFixture;
        private FlightEntity _flightEntityFixture;
        private FlightDto _flightDtoFixture;

        public FlightServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _flightEntitieListFixture = _fixture.CreateMany<FlightEntity>(2).ToList();
            _flightEntityFixture = _fixture.Create<FlightEntity>();
            _flightDtoFixture = _fixture.Create<FlightDto>();


            _mockFlightRepository = new Mock<IFlightRepository>();

            _flightService = new FlightService(_mockFlightRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.GetAllAsync())
                                  .ReturnsAsync(_flightEntitieListFixture);

            var expectation = _mapper.Map<List<FlightDto>>(_flightEntitieListFixture);

            // Act
            var result = await _flightService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<FlightDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectation);

            _mockFlightRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(_flightEntitieListFixture[0]);

            var expectation = _mapper.Map<FlightDto>(_flightEntitieListFixture[0]);

            // Act
            var result = await _flightService.GetByIdAsync(_flightEntityFixture.Id);

            // Assert
            result.Should().BeOfType<FlightDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockFlightRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
        {
            // Arrange
            _flightEntityFixture = null!;

            _mockFlightRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(_flightEntityFixture);

            // Act
            var result = await _flightService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockFlightRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightId()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.CreateAsync(It.IsAny<FlightEntity>()))
                                  .ReturnsAsync(_flightDtoFixture.Id);

            // Act
            var result = await _flightService.CreateAsync(_flightDtoFixture);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_flightDtoFixture.Id);

            _mockFlightRepository.Verify(a => a.CreateAsync(It.IsAny<FlightEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.CreateAsync(It.IsAny<FlightEntity>()))
                                  .ReturnsAsync(Guid.Empty);

            // Act
            var resuilt = await _flightService.CreateAsync(new FlightDto());

            // Assert
            resuilt.Should().BeEmpty();

            _mockFlightRepository.Verify(a => a.CreateAsync(It.IsAny<FlightEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightEntity>()))
                                  .ReturnsAsync(_flightDtoFixture.Id);

            // Act
            var result = await _flightService.UpdateAsync(_flightDtoFixture.Id, _flightDtoFixture);

            // Asert
            result.Should().NotBeEmpty();
            result.Should().Be(_flightDtoFixture.Id);

            _mockFlightRepository.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<FlightEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _flightEntityFixture.Id = Guid.Empty;

            _mockFlightRepository.Setup(config => config.UpdateAsync(Guid.Empty, _flightEntityFixture))
                                  .ReturnsAsync(_flightDtoFixture.Id);

            // Act
            var result = await _flightService.UpdateAsync(Guid.Empty, _flightDtoFixture);

            // Assert
            result.Should().BeEmpty();

            _mockFlightRepository.Verify(a => a.UpdateAsync(Guid.Empty, _flightEntityFixture), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_ONSuccess_Returns_True()
        {
            // Arrange
            _mockFlightRepository.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(true);

            // Act
            var result = await _flightService.DeleteAsync(_flightDtoFixture.Id);

            // Assert
            result.Should().BeTrue();

            _mockFlightRepository.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
