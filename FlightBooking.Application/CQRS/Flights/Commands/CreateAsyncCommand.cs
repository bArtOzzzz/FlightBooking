using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Commands
{
    public record class CreateAsyncCommand(FlightDto FlightDto) : IRequest<Guid>;
}
