namespace FlightBooking.API.Models.Response
{
    public class FlightResponse
    {
        public Guid AirlineId { get; set; }
        public Guid AirplaneId { get; set; }
        public string? Departurer { get; set; }
        public string? Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivingDate { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedTime { get; set; }
    }
}
