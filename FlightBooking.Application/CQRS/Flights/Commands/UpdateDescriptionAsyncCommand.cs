using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Commands
{
    public record class UpdateDescriptionAsyncCommand(Guid Id, FlightDto FlightDto) : IRequest<Guid>;
}
