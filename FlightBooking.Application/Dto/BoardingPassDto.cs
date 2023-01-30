namespace FlightBooking.Application.Dto
{
    public class BoardingPassDto
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public decimal Prise { get; set; }
        public bool isExpired { get; set; }
        public DateTime BookingExpireDate { get; set; }
    }
}
