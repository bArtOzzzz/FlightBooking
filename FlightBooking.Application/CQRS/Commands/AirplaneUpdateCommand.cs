using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class AirplaneUpdateCommand(Guid id, AirplaneDto AirplaneDto) : IRequest<Guid>;
}
