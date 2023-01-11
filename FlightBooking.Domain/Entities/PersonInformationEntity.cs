namespace FlightBooking.Domain.Entities
{
    public class PersonInformationEntity
    {
        public Guid Id { get; set; }
        public string Citizenship { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime ExpirePasportDate { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public string BirthDate { get; set; }
        public string Gender { get; set; }
    }
}
