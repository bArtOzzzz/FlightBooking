using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record AirlineUpdateCommand(Guid Id, AirlineDto AirlineDto) : IRequest<Guid>;
}
