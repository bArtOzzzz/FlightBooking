using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Commands
{
    public record class DeleteAsyncCommand(Guid Id) : IRequest<bool>;
}
