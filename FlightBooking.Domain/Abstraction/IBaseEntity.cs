namespace FlightBooking.Domain.Abstraction
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
