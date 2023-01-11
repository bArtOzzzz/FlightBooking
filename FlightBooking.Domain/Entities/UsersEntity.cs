using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class UsersEntity : BaseEntity
    {
        public PersonInformationEntity? PersonInformation { get; set; }
        public List<BoardingPassEntity>? BoardingPasses { get; set; }
    }
}
