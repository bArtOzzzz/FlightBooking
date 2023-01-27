using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Application.Dto;
using FluentAssertions;
using AutoFixture;
using AutoMapper;
using Xunit;
using Moq;

namespace FlightBooking.Test.UserTest
{
    public class UserServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture = new();

        private readonly Mock<IUserRepository> _mockUserRepository;

        private readonly UserService _userService;

        private List<UsersEntity> _userEntitieListFixture;
        private UsersEntity _userEntityFixture;

        public UserServiceTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _userEntitieListFixture = _fixture.CreateMany<UsersEntity>(2).ToList();
            _userEntityFixture = _fixture.Create<UsersEntity>();


            _mockUserRepository = new Mock<IUserRepository>();

            _userService = new UserService(_mockUserRepository.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Return_RightCollectionAndType()
        {
            // Arrange
            _mockUserRepository.Setup(config => config.GetAllAsync())
                                   .ReturnsAsync(_userEntitieListFixture);

            var expectation = _mapper.Map<List<UserDto>>(_userEntitieListFixture);

            // Act
            var result = await _userService.GetAllAsync();

            // Assert
            result.Should().BeOfType<List<UserDto>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectation);

            _mockUserRepository.Verify(p => p.GetAllAsync(), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightObjectAndType()
        {
            // Arrange
            _mockUserRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(_userEntitieListFixture[0]);

            var expectation = _mapper.Map<UserDto>(_userEntitieListFixture[0]);

            // Act
            var result = await _userService.GetByIdAsync(_userEntityFixture.Id);

            // Assert
            result.Should().BeOfType<UserDto>();
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectation);

            _mockUserRepository.Verify(a => a.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenAirlineIdIsNotCorrect_Returns_Null()
        {
            // Arrange
            _userEntityFixture = null!;

            _mockUserRepository.Setup(config => config.GetByIdAsync(It.IsAny<Guid>()))
                                   .ReturnsAsync(_userEntityFixture);

            // Act
            var result = await _userService.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();

            _mockUserRepository.Verify(p => p.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
