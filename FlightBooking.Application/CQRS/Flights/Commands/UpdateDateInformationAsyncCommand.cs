using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Commands
{
    public record class UpdateDateInformationAsyncCommand(Guid Id, FlightDto FlightDto) : IRequest<Guid>;
}
