using MediatR;

namespace FlightBooking.Application.CQRS.Commands.UpdateCommands
{
    public class UpdateAirlineCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
    }
}
