namespace FlightBooking.Domain.Entities
{
    public class AirplaneEntity
    {
        public Guid Id { get; set; }
        public string? ModelName { get; set; }
        public int MaximumSeats { get; set; }
        public int MaximumWeight { get; set; }
        public List<FlightEntity>? Flights { get; set; }
    }
}
