using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class AirplaneEntity : BaseEntity
    {
        public string? ModelName { get; set; }
        public int MaximumSeats { get; set; }
        public int MaximumWeight { get; set; }
        public List<FlightEntity>? Flights { get; set; }
    }
}
