using MediatR;

namespace FlightBooking.Application.CQRS.Commands.CreateCommands
{
    public class CreateAirlineCommand : IRequest<Guid>
    {
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
    }
}
