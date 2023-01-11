using MediatR;

namespace FlightBooking.Application.CQRS.Commands.DeleteCommands
{
    public class DeleteAirlineCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
