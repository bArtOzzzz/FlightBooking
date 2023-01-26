using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? CreatedTime { get; set; }
    }
}
