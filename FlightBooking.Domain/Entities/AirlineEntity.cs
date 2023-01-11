namespace FlightBooking.Domain.Entities
{
    public class AirlineEntity
    {
        public Guid Id { get; set; }
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
        public List<FlightEntity>? Flights { get; set; }
    }
}
