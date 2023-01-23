using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Commands
{
    public record UpdateAsyncCommand(Guid Id, AirlineDto AirlineDto) : IRequest<Guid>;
}
