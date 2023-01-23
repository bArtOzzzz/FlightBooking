using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Commands
{
    public record DeleteAsyncCommand(Guid Id) : IRequest<bool>;
}
