namespace FlightBooking.API.Models.Request
{
    public class BoardingPassCreateRequest
    {
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public decimal Prise { get; set; }
        public bool isExpired { get; set; }
        public string? BookingExpireDate { get; set; }
    }
}
