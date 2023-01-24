using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record AirlineDeleteAsyncCommand(Guid Id) : IRequest<bool>;
}
