using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class AirlineEntity : BaseEntity
    {
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
        public List<FlightEntity>? Flights { get; set; }
    }
}
