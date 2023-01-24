using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class BoardingPassCreateCommand(BoardingPassDto BoardingPassDto) : IRequest<Guid>;
}
