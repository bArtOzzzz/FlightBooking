namespace FlightBooking.Domain.Abstraction
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        string? CreatedDate { get; set; }
        string? CreatedTime { get; set; }
    }
}
