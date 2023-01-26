using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record AirlineDeleteCommand(Guid Id) : IRequest<bool>;
}
