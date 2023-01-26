using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IPersonInformationService
    {
        /// <summary>
        /// Creates mapping for data between PersonInformationDto and PersonInformationEntity
        /// Gets all Persons Information data from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<PersonInformationDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between PersonInformationDto and PesonInformationEntity
        /// Gets Person Information data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PersonInformationDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates mapping for data between PersonInformationDto and PersonInformationEntity
        /// Create new Person Information and User from repository
        /// </summary>
        /// <param name="personInformation"></param>
        /// <returns></returns>
        public Task<Guid[]> CreateAsync(PersonInformationDto personInformation);

        /// <summary>
        /// Creates mapping for data between PersonInformationDto and PersonInformationEntity
        /// Updates Person Information data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personInformation"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, PersonInformationDto personInformation);

        /// <summary>
        /// Deletes Person Information and User by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
