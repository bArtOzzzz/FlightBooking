namespace FlightBooking.Domain.Entities
{
    public class BoardingPassEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Prise { get; set; }
        public bool isExpired { get; set; }
        public DateTime BookingExpireDate { get; set; }
    }
}
