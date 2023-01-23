namespace FlightBooking.API.Models.Request
{
    public class FlightCreateOrUpdateRequest
    {
        public Guid AirlineId { get; set; }
        public Guid AirplaneId { get; set; }
        public string? Departurer { get; set; }
        public string? Arrival { get; set; }
        public string? DepartureDate { get; set; }
        public string? ArrivingDate { get; set; }
    }
}
