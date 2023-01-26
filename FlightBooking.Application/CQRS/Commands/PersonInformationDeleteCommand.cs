using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class PersonInformationDeleteCommand(Guid id) : IRequest<bool>;
}
