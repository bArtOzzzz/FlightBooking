namespace FlightBooking.API.Models.Request
{
    public class PersonInformationCreateOrUpdateRequest
    {
        public string? Citizenship { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? ExpirePasportDate { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? BirthDate { get; set; }
        public string? Gender { get; set; }
    }
}
