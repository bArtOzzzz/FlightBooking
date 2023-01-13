using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Commands
{
    public record DeleteAirlineCommand(Guid Id) : IRequest<bool>;
}
