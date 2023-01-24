using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class AirplaneDeleteCommand(Guid id) : IRequest<bool>;
}
