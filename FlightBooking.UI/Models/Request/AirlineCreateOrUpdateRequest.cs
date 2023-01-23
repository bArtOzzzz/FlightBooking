namespace FlightBooking.API.Models.Request
{
    public class AirlineCreateOrUpdateRequest
    {
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
    }
}
