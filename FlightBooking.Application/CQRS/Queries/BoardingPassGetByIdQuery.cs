using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record class BoardingPassGetByIdQuery(Guid id) : IRequest<BoardingPassDto>;
}
