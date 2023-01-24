using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class AirplaneCreateCommand(AirplaneDto AirplaneDto) : IRequest<Guid>;
}
