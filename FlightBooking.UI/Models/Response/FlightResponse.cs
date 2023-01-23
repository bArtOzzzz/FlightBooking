namespace FlightBooking.API.Models.Response
{
    public class FlightResponse
    {
        public string? Departurer { get; set; }
        public string? Arrival { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivingDate { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedTime { get; set; }
    }
}
