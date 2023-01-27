using FlightBooking.Application.CQRS.Queries;
using FlightBooking.API.Models.Response;
using FlightBooking.API.Controllers;
using FlightBooking.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using AutoFixture;
using AutoMapper;
using MediatR;
using Xunit;
using Moq;

namespace FlightBooking.Test.UserTest
{
    public class UserControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly UserController _userController;

        private readonly IMapper _mapper;

        private List<UserDto> _usersDtoListFixture;
        private readonly UserDto _userDtoFixture;

        public UserControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper();

            _mockMediator = new Mock<IMediator>();

            _usersDtoListFixture = _fixture.CreateMany<UserDto>(3).ToList();
            _userDtoFixture = _fixture.Create<UserDto>();

            _userController = new UserController(_mockMediator.Object, _mapper);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<UserResponse>>(_usersDtoListFixture);

            _mockMediator.Setup(m => m.Send(new UserGetAllQuery(), default))
                         .ReturnsAsync(_usersDtoListFixture);

            // Act
            var result = (ObjectResult)await _userController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<UserResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _usersDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new UserGetAllQuery(), default))
                         .ReturnsAsync(_usersDtoListFixture);

            // Act
            var result = await _userController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new UserGetAllQuery(), default))
                         .ReturnsAsync(new List<UserDto>());

            // Act
            var result = await _userController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<UserResponse>(_userDtoFixture);

            _mockMediator.Setup(m => m.Send(new UserGetByIdQuery(_userDtoFixture.Id), default))
                         .ReturnsAsync(_userDtoFixture);

            // Act
            var result = (ObjectResult)await _userController.GetByIdAsync(_userDtoFixture.Id);

            // Assert
            result.Value.Should().BeOfType<UserResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new UserGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _userController.GetByIdAsync(_userDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new UserGetByIdQuery(_userDtoFixture.Id), default))
                         .ReturnsAsync(_userDtoFixture);

            // Act
            var result = await _userController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<UserGetByIdQuery>(), default), Times.Once);
        }
    }
}
