namespace FlightBooking.Application.Dto
{
    public class AirplaneDto
    {
        public Guid Id { get; set; }
        public string? ModelName { get; set; }
        public int MaximumSeats { get; set; }
        public int MaximumWeight { get; set; }
    }
}
