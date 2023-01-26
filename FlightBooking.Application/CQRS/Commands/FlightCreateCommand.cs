using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class FlightCreateCommand(FlightDto FlightDto) : IRequest<Guid>;
}
