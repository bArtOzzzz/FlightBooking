using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.API.Models.Response;
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

namespace FlightBooking.Test.PersonInformationTest
{
    public class PersonInformationControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        private readonly Fixture _fixture = new();
        private readonly PersonInformationController _personInformationController;

        private readonly Mock<IValidator<PersonInformationCreateOrUpdateRequest>> _mockValidator;

        private readonly IMapper _mapper;

        private List<PersonInformationDto> _personInformationDtoListFixture;
        private readonly PersonInformationDto _personInformationDtoFixture;
        private readonly PersonInformationCreateOrUpdateRequest _personInformationCreateOrUpdateFixture;

        public PersonInformationControllerTest()
        {
            MapperConfiguration mappingConfig = new(mc => mc.AddProfile(new Application.Mapper.Mapper()));
            _mapper = mappingConfig.CreateMapper(); 

            _mockMediator = new Mock<IMediator>();
            _mockValidator = new Mock<IValidator<PersonInformationCreateOrUpdateRequest>>();

            _personInformationDtoListFixture = _fixture.CreateMany<PersonInformationDto>(3).ToList();
            _personInformationDtoFixture = _fixture.Create<PersonInformationDto>();
            _personInformationCreateOrUpdateFixture = _fixture.Create<PersonInformationCreateOrUpdateRequest>();

            _personInformationController = new PersonInformationController(_mockMediator.Object, _mapper, _mockValidator.Object);
        }

        [Fact]
        private async Task GetAllAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<List<PersonInformationResponse>>(_personInformationDtoListFixture);

            _mockMediator.Setup(m => m.Send(new PersonInformationGetAllQuery(), default))
                         .ReturnsAsync(_personInformationDtoListFixture);

            // Act
            var result = (ObjectResult)await _personInformationController.GetAllAsync();

            // Assert
            result.Value.Should().BeOfType<List<PersonInformationResponse>>();
            result.Value.Should().BeEquivalentTo(expectedResponse);
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsNull_Returns_NotFoundResult()
        {
            // Arrange
            _personInformationDtoListFixture = null!;

            _mockMediator.Setup(m => m.Send(new PersonInformationGetAllQuery(), default))
                         .ReturnsAsync(_personInformationDtoListFixture);

            // Act
            var result = await _personInformationController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetAllAsync_WhenCollectionIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationGetAllQuery(), default))
                         .ReturnsAsync(new List<PersonInformationDto>());

            // Act
            var result = await _personInformationController.GetAllAsync();

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetAllQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            var expectedResponse = _mapper.Map<PersonInformationResponse>(_personInformationDtoFixture);

            _mockMediator.Setup(m => m.Send(new PersonInformationGetByIdQuery(_personInformationDtoFixture.Id), default))
                         .ReturnsAsync(_personInformationDtoFixture);

            // Act
            var result = (ObjectResult)await _personInformationController.GetByIdAsync(_personInformationDtoFixture.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<PersonInformationResponse>();
            result.Value.Should().BeEquivalentTo(expectedResponse);

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationGetByIdQuery(Guid.Empty), default));

            // Act
            var result = await _personInformationController.GetByIdAsync(_personInformationDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task GetByIdAsync_WhenControllerRequestIsEmpty_Returns_NotFoundResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationGetByIdQuery(_personInformationDtoFixture.Id), default))
                         .ReturnsAsync(_personInformationDtoFixture);

            // Act
            var result = await _personInformationController.GetByIdAsync(Guid.Empty);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationGetByIdQuery>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_OnSuccess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationCreateCommand(_personInformationDtoFixture), default))
                         .ReturnsAsync(new Guid[] { _personInformationDtoFixture.Id, _personInformationDtoFixture.UserId });

            PersonInformationCreateOrUpdateRequest personInformationCreateOrUpdateRequest = new()
            {
                Citizenship = "America",
                IdentificationNumber = "2763984J836PB3",
                ExpirePasportDate = new DateTime(2024, 1, 15).ToString(),
                Name = "Владислав",
                Surname = "Лазарев",
                BirthDate = new DateTime(1993, 6, 11).ToString(),
                Gender = "М",
            };

            // Act
            var result = (ObjectResult)await _personInformationController.CreateAsync(personInformationCreateOrUpdateRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid[]>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationCreateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task CreateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _personInformationController.CreateAsync(null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationCreateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_OnSuccsess_Returns_RightType_And_OkResult()
        {
            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationUpdateCommand(_personInformationDtoFixture.Id, _personInformationDtoFixture), default))
                         .ReturnsAsync(_personInformationDtoFixture.Id);

            PersonInformationCreateOrUpdateRequest personInformationCreateOrUpdateRequest = new()
            {
                Citizenship = "America",
                IdentificationNumber = "2763984J836PB3",
                ExpirePasportDate = new DateTime(2024, 1, 15).ToString(),
                Name = "Владислав",
                Surname = "Лазарев",
                BirthDate = new DateTime(1993, 6, 11).ToString(),
                Gender = "М",
            };

            // Act
            var result = (ObjectResult)await _personInformationController.UpdateAsync(_personInformationDtoFixture.Id, personInformationCreateOrUpdateRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Guid>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationUpdateCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task UpdateAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            // Act
            var result = await _personInformationController.UpdateAsync(Guid.Empty, _personInformationCreateOrUpdateFixture);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task UpdateAsync_WhenRequestIsNull_Returns_NotFoundResult()
        {
            // Act
            var result = await _personInformationController.UpdateAsync(_personInformationDtoFixture.Id, null!);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationUpdateCommand>(), default), Times.Never);
        }

        [Fact]
        private async Task DeleteAsync_OnSuccess_Returns_RightType_And_NoContentResult()
        {
            // Act
            var result = await _personInformationController.DeleteAsync(_personInformationDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationDeleteCommand>(), default), Times.Once);
        }

        [Fact]
        private async Task DeleteAsync_WhenIdIsEmpty_Returns_NotFoundResult()
        {
            _personInformationDtoFixture.Id = Guid.Empty;

            // Arrange
            _mockMediator.Setup(m => m.Send(new PersonInformationDeleteCommand(_personInformationDtoFixture.Id), default))
                         .ReturnsAsync(true);

            // Act
            var result = await _personInformationController.DeleteAsync(_personInformationDtoFixture.Id);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockMediator.Verify(x => x.Send(It.IsAny<PersonInformationDeleteCommand>(), default), Times.Never);
        }
    }
}
