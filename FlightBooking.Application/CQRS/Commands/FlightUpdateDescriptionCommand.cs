using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class FlightUpdateDescriptionCommand(Guid Id, FlightDto FlightDto) : IRequest<Guid>;
}
