namespace FlightBooking.API.Models.Request
{
    public class AirplaneCreateOrUpdateRequest
    {
        public string? ModelName { get; set; }
        public int MaximumSeats { get; set; }
        public int MaximumWeight { get; set; }
    }
}
