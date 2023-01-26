using FlightBooking.Domain.Abstraction;

namespace FlightBooking.Domain.Entities
{
    public class PersonInformationEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public string? Citizenship { get; set; }
        public string? IdentificationNumber { get; set; }
        public DateTime ExpirePasportDate { get; set; }
        public string? Name { get; set; }    
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Gender { get; set; }
        public UsersEntity? User { get; set; }
    }
}
