namespace FlightBooking.Domain.Entities
{
    public class FlightEntity
    {
        public Guid Id { get; set; }
        public Guid AirlineId { get; set; }
        public Guid AirplaneId { get; set; }
        public string? Departurer { get; set; }
        public string? Arrival { get; set; }
        public DateTime DepartureDay { get; set; }
        public DateTime DepareingAt { get; set; }
        public DateTime ArrivingAt { get; set; }
        public AirlineEntity? Airlines { get; set;}
        public AirplaneEntity? Airplane { get; set; }
    }
}
