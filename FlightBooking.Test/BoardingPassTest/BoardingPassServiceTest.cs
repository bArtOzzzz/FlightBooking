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

namespace FlightBooking.Test.BoardingPassTest
{
    public class BoardingPassServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IBoardingPassRepository> _mockBoardingPassRepository;

        private readonly BoardingPassService _boardingPassService;

        private List<BoardingPassEntity> _boardingPassEntitieListFixture;
        private BoardingPassEntity _boardingPassEntityFixture;
        private BoardingPassDto _boardingPassDtoFixture;

        public BoardingPassServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _boardingPassEntitieListFixture = _fixture.CreateMany<BoardingPassEntity>(2).ToList();
            _boardingPassEntityFixture = _fixture.Create<BoardingPassEntity>();
            _boardingPassDtoFixture = _fixture.Create<BoardingPassDto>();


            _mockBoardingPassRepository = new Mock<IBoardingPassRepository>();

            _boardingPassService = new BoardingPassService(_mockBoardingPassRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.GetAllAsync())
                                       .ReturnsAsync(_boardingPassEntitieListFixture);

            var expectation = _mapper.Map<List<BoardingPassDto>>(_boardingPassEntitieListFixture);

            // Act
            var result = await _boardingPassService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<BoardingPassDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectation);

            _mockBoardingPassRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(_boardingPassEntitieListFixture[0]);

            var expectation = _mapper.Map<BoardingPassDto>(_boardingPassEntitieListFixture[0]);

            // Act
            var result = await _boardingPassService.GetByIdAsync(_boardingPassEntityFixture.Id);

            // Assert
            result.Should().BeOfType<BoardingPassDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockBoardingPassRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
        {
            // Arrange
            _boardingPassEntityFixture = null!;

            _mockBoardingPassRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(_boardingPassEntityFixture);

            // Act
            var result = await _boardingPassService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockBoardingPassRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightId()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.CreateAsync(It.IsAny<BoardingPassEntity>()))
                                       .ReturnsAsync(_boardingPassDtoFixture.Id);

            // Act
            var result = await _boardingPassService.CreateAsync(_boardingPassDtoFixture);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().Be(_boardingPassDtoFixture.Id);

            _mockBoardingPassRepository.Verify(a => a.CreateAsync(It.IsAny<BoardingPassEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.CreateAsync(It.IsAny<BoardingPassEntity>()))
                                      .ReturnsAsync(Guid.Empty);

            // Act
            var resuilt = await _boardingPassService.CreateAsync(new BoardingPassDto());

            // Assert
            resuilt.Should().BeEmpty();

            _mockBoardingPassRepository.Verify(a => a.CreateAsync(It.IsAny<BoardingPassEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassEntity>()))
                                       .ReturnsAsync(_boardingPassDtoFixture.Id);

            // Act
            var result = await _boardingPassService.UpdateAsync(_boardingPassDtoFixture.Id, _boardingPassDtoFixture);

            // Asert
            result.Should().NotBeEmpty();
            result.Should().Be(_boardingPassDtoFixture.Id);

            _mockBoardingPassRepository.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<BoardingPassEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _boardingPassEntityFixture.Id = Guid.Empty;

            _mockBoardingPassRepository.Setup(config => config.UpdateAsync(Guid.Empty, _boardingPassEntityFixture))
                                       .ReturnsAsync(_boardingPassDtoFixture.Id);

            // Act
            var result = await _boardingPassService.UpdateAsync(Guid.Empty, _boardingPassDtoFixture);

            // Assert
            result.Should().BeEmpty();

            _mockBoardingPassRepository.Verify(a => a.UpdateAsync(Guid.Empty, _boardingPassEntityFixture), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_ONSuccess_Returns_True()
        {
            // Arrange
            _mockBoardingPassRepository.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(true);

            // Act
            var result = await _boardingPassService.DeleteAsync(_boardingPassDtoFixture.Id);

            // Assert
            result.Should().BeTrue();

            _mockBoardingPassRepository.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
