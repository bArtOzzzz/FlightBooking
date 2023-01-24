using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record AirlineCreateCommand(AirlineDto AirlineDto) : IRequest<Guid>;
}
