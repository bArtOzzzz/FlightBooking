using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Services;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using AutoFixture;
using AutoMapper;
using Xunit;
using Moq;

namespace FlightBooking.Test.AirlineTests
{
    public class AirlineServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IAirlineRepository> _mockAirlineRepository;

        private readonly AirlineService _airlineService;

        private List<AirlineEntity> _airlineEntitieListFixture;
        private AirlineEntity _airlineEntityFixture;
        private AirlineDto _airlineDtoFixture;

        public AirlineServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _airlineEntitieListFixture = _fixture.CreateMany<AirlineEntity>(2).ToList();
            _airlineEntityFixture = _fixture.Create<AirlineEntity>();
            _airlineDtoFixture = _fixture.Create<AirlineDto>();


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
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
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

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightId()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.CreateAsync(It.IsAny<AirlineEntity>()))
                                  .ReturnsAsync(_airlineDtoFixture.Id);

            // Act
            var result = await _airlineService.CreateAsync(_airlineDtoFixture);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_airlineDtoFixture.Id);

            _mockAirlineRepository.Verify(a => a.CreateAsync(It.IsAny<AirlineEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.CreateAsync(It.IsAny<AirlineEntity>()))
                                  .ReturnsAsync(Guid.Empty);

            // Act
            var resuilt = await _airlineService.CreateAsync(new AirlineDto());

            // Assert
            resuilt.Should().BeEmpty();

            _mockAirlineRepository.Verify(a => a.CreateAsync(It.IsAny<AirlineEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineEntity>()))
                                  .ReturnsAsync(_airlineDtoFixture.Id);

            // Act
            var result = await _airlineService.UpdateAsync(_airlineDtoFixture.Id, _airlineDtoFixture);

            // Asert
            result.Should().NotBeEmpty();
            result.Should().Be(_airlineDtoFixture.Id);

            _mockAirlineRepository.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirlineEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _airlineEntityFixture.Id = Guid.Empty;

            _mockAirlineRepository.Setup(config => config.UpdateAsync(Guid.Empty, _airlineEntityFixture))
                                  .ReturnsAsync(_airlineDtoFixture.Id);

            // Act
            var result = await _airlineService.UpdateAsync(Guid.Empty, _airlineDtoFixture);

            // Assert
            result.Should().BeEmpty();

            _mockAirlineRepository.Verify(a => a.UpdateAsync(Guid.Empty, _airlineEntityFixture), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_ONSuccess_Returns_True()
        {
            // Arrange
            _mockAirlineRepository.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(true);

            // Act
            var result = await _airlineService.DeleteAsync(_airlineDtoFixture.Id);

            // Assert
            result.Should().BeTrue();

            _mockAirlineRepository.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
