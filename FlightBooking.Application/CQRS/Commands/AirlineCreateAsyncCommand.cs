using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record AirlineCreateAsyncCommand(AirlineDto AirlineDto) : IRequest<Guid>;
}
