using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class BoardingPassDeleteCommand(Guid id) : IRequest<bool>;
}
