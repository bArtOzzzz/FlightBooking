using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Application.Dto;
using FluentAssertions;
using AutoFixture;
using AutoMapper;
using Xunit;
using Moq;

namespace FlightBooking.Test.PersonInformationTest
{
    public class PersonInformationServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IPersonInformationRepository> _mockPersonInformationRepository;

        private readonly PersonInformationService _personInformationService;

        private List<PersonInformationEntity> _personInformationEntitieListFixture;
        private PersonInformationEntity _personInformationEntityFixture;
        private PersonInformationDto _personInformationDtoFixture;

        public PersonInformationServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _personInformationEntitieListFixture = _fixture.CreateMany<PersonInformationEntity>(2).ToList();
            _personInformationEntityFixture = _fixture.Create<PersonInformationEntity>();
            _personInformationDtoFixture = _fixture.Create<PersonInformationDto>();


            _mockPersonInformationRepository = new Mock<IPersonInformationRepository>();

            _personInformationService = new PersonInformationService(_mockPersonInformationRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.GetAllAsync())
                                            .ReturnsAsync(_personInformationEntitieListFixture);

            var expectation = _mapper.Map<List<PersonInformationDto>>(_personInformationEntitieListFixture);

            // Act
            var result = await _personInformationService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<PersonInformationDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(expectation);

            _mockPersonInformationRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                            .ReturnsAsync(_personInformationEntitieListFixture[0]);

            var expectation = _mapper.Map<PersonInformationDto>(_personInformationEntitieListFixture[0]);

            // Act
            var result = await _personInformationService.GetByIdAsync(_personInformationEntityFixture.Id);

            // Assert
            result.Should().BeOfType<PersonInformationDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockPersonInformationRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
        {
            // Arrange
            _personInformationEntityFixture = null!;

            _mockPersonInformationRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                            .ReturnsAsync(_personInformationEntityFixture);

            // Act
            var result = await _personInformationService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockPersonInformationRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightId()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.CreateAsync(It.IsAny<PersonInformationEntity>()))
                                            .ReturnsAsync(new Guid[] { _personInformationDtoFixture.Id, _personInformationDtoFixture.UserId });

            // Act
            var result = await _personInformationService.CreateAsync(_personInformationDtoFixture);

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);

            _mockPersonInformationRepository.Verify(a => a.CreateAsync(It.IsAny<PersonInformationEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.CreateAsync(It.IsAny<PersonInformationEntity>()))
                                  .ReturnsAsync(new Guid[] {  });

            // Act
            var result = await _personInformationService.CreateAsync(new PersonInformationDto());

            // Assert
            result.Should().BeEmpty();
            result.Should().HaveCount(0);

            _mockPersonInformationRepository.Verify(a => a.CreateAsync(It.IsAny<PersonInformationEntity>()), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenAirlineIsEmpty1_Returns_Empty()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.CreateAsync(It.IsAny<PersonInformationEntity>()))
                                  .ReturnsAsync(new Guid[] { Guid.Empty, Guid.Empty });

            // Act
            var result = await _personInformationService.CreateAsync(new PersonInformationDto());

            // Assert
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result[0].Should().BeEmpty();
            result[1].Should().BeEmpty();

            _mockPersonInformationRepository.Verify(a => a.CreateAsync(It.IsAny<PersonInformationEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationEntity>()))
                                  .ReturnsAsync(_personInformationDtoFixture.Id);

            // Act
            var result = await _personInformationService.UpdateAsync(_personInformationDtoFixture.Id, _personInformationDtoFixture);

            // Asert
            result.Should().NotBeEmpty();
            result.Should().Be(_personInformationDtoFixture.Id);

            _mockPersonInformationRepository.Verify(a => a.UpdateAsync(It.IsAny<Guid>(), It.IsAny<PersonInformationEntity>()), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenAirlineIsEmpty_Returns_Empty()
        {
            // Arrange
            _personInformationEntityFixture.Id = Guid.Empty;

            _mockPersonInformationRepository.Setup(config => config.UpdateAsync(Guid.Empty, _personInformationEntityFixture))
                                  .ReturnsAsync(_personInformationDtoFixture.Id);

            // Act
            var result = await _personInformationService.UpdateAsync(Guid.Empty, _personInformationDtoFixture);

            // Assert
            result.Should().BeEmpty();

            _mockPersonInformationRepository.Verify(a => a.UpdateAsync(Guid.Empty, _personInformationEntityFixture), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_ONSuccess_Returns_True()
        {
            // Arrange
            _mockPersonInformationRepository.Setup(config => config.DeleteAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(true);

            // Act
            var result = await _personInformationService.DeleteAsync(_personInformationDtoFixture.Id);

            // Assert
            result.Should().BeTrue();

            _mockPersonInformationRepository.Verify(a => a.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
