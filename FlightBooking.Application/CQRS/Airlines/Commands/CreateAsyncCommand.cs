using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Commands
{
    public record CreateAsyncCommand(AirlineDto AirlineDto) : IRequest<Guid>;
}
