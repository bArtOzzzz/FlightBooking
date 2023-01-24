using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class FlightDeleteCommand(Guid Id) : IRequest<bool>;
}
