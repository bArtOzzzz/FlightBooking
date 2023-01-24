namespace FlightBooking.API.Models.Request
{
    public class BoardingPassUpdateRequest
    {
        public bool isExpired { get; set; }
        public string? BookingExpireDate { get; set; }
    }
}
