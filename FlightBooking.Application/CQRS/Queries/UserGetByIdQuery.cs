using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record class UserGetByIdQuery(Guid id) : IRequest<UserDto>;
}
