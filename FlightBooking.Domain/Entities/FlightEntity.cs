using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class FlightEntity : BaseEntity
    {
        public Guid AirlineId { get; set; }
        public Guid AirplaneId { get; set; }
        public string? Departurer { get; set; }
        public string? Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivingDate { get; set; }
        public AirlineEntity? Airlines { get; set;}
        public AirplaneEntity? Airplane { get; set; }
        public List<BoardingPassEntity>? BoardingPasses { get; set; }
    }
}
