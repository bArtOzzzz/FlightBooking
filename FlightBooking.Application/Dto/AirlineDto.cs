namespace FlightBooking.Application.Dto
{
    public class AirlineDto
    {
        public Guid Id { get; set; }
        public string? AirlineName { get; set; }
        public double Rating { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedTime { get; set; }
    }
}
