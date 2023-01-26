namespace FlightBooking.API.Models.Response
{
    public class BoardingPassResponse
    {
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public decimal Prise { get; set; }
        public bool isExpired { get; set; }
        public DateTime BookingExpireDate { get; set; }
    }
}
