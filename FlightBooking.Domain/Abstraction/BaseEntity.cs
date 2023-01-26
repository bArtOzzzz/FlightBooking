namespace FlightBooking.Domain.Abstraction
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedTime { get; set; }
    }
}
