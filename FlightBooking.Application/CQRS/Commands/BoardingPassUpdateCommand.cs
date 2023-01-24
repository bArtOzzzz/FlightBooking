using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class BoardingPassUpdateCommand(Guid id, BoardingPassDto BoardingPassDto) : IRequest<Guid>;
}
