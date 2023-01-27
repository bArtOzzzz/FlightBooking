using AutoFixture;
using AutoMapper;
using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using FlightBooking.Application.Services;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace FlightBooking.Test.AirplaneTest
{
    public class AirplaneSerciceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IAirplaneRepository> _mockAirplaneRepository;

        private readonly AirplaneService _airplaneService;

        private List<AirplaneEntity> _airplaneEntitieListFixture;
        private AirplaneEntity _airplaneEntityFixture;
        private AirplaneDto _airplaneDtoFixture;

        public AirplaneSerciceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _airplaneEntitieListFixture = _fixture.CreateMany<AirplaneEntity>(2).ToList();
            _airplaneEntityFixture = _fixture.Create<AirplaneEntity>();
            _airplaneDtoFixture = _fixture.Create<AirplaneDto>();


            _mockAirplaneRepository = new Mock<IAirplaneRepository>();

            _airplaneService = new AirplaneService(_mockAirplaneRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.GetAllAsync())
                                   .ReturnsAsync(_airplaneEntitieListFixture);

            var expectation = _mapper.Map<List<AirplaneDto>>(_airplaneEntitieListFixture);

            // Act
            var result = await _airplaneService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<AirplaneDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectation);

            _mockAirplaneRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(_airplaneEntitieListFixture[0]);

            var expectation = _mapper.Map<AirplaneDto>(_airplaneEntitieListFixture[0]);

            // Act
            var result = await _airplaneService.GetByIdAsync(_airplaneEntityFixture.Id);

            // Assert
            result.Should().BeOfType<AirplaneDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockAirplaneRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
        {
            // Arrange
            _airplaneEntityFixture = null!;

            _mockAirplaneRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(_airplaneEntityFixture);

            // Act
            var result = await _airplaneService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockAirplaneRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightId()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.CreateAsync(It.IsAny<AirplaneEntity>()))
                                   .ReturnsAsync(_airplaneDtoFixture.Id);

            // Act
            var result = await _airplaneService.CreateAsync(_airplaneDtoFixture);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_airplaneDtoFixture.Id);

            _mockAirplaneRepository.Verify(a => a.CreateAsync(It.IsAny<AirplaneEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.CreateAsync(It.IsAny<AirplaneEntity>()))
                                   .ReturnsAsync(Guid.Empty);

            // Act
            var resuilt = await _airplaneService.CreateAsync(new AirplaneDto());

            // Assert
            resuilt.Should().BeEmpty();

            _mockAirplaneRepository.Verify(a => a.CreateAsync(It.IsAny<AirplaneEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneEntity>()))
                                   .ReturnsAsync(_airplaneDtoFixture.Id);

            // Act
            var result = await _airplaneService.UpdateAsync(_airplaneDtoFixture.Id, _airplaneDtoFixture);

            // Asert
            result.Should().NotBeEmpty();
            result.Should().Be(_airplaneDtoFixture.Id);

            _mockAirplaneRepository.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<AirplaneEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _airplaneEntityFixture.Id = Guid.Empty;

            _mockAirplaneRepository.Setup(config => config.UpdateAsync(Guid.Empty, _airplaneEntityFixture))
                                   .ReturnsAsync(_airplaneDtoFixture.Id);

            // Act
            var result = await _airplaneService.UpdateAsync(Guid.Empty, _airplaneDtoFixture);

            // Assert
            result.Should().BeEmpty();

            _mockAirplaneRepository.Verify(a => a.UpdateAsync(Guid.Empty, _airplaneEntityFixture), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_ONSuccess_Returns_True()
        {
            // Arrange
            _mockAirplaneRepository.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(true);

            // Act
            var result = await _airplaneService.DeleteAsync(_airplaneDtoFixture.Id);

            // Assert
            result.Should().BeTrue();

            _mockAirplaneRepository.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
