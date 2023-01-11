using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class BoardingPassEntity : BaseEntity
    {
        public Guid FlightId { get; set; }
        public Guid UserId { get; set; }
        public decimal Prise { get; set; }
        public bool isExpired { get; set; }
        public DateTime BookingExpireDate { get; set; }
        public FlightEntity? Flight { get; set; }
        public UsersEntity? User { get; set; }
    }
}
