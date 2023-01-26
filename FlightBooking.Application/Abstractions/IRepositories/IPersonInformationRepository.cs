using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepositories
{
    public interface IPersonInformationRepository
    {
        /// <summary>
        /// Gets all users information
        /// </summary>
        /// <returns></returns>
        public Task<List<PersonInformationEntity>> GetAllAsync();

        /// <summary>
        /// Gets user information by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<PersonInformationEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates new 
        /// </summary>
        /// <param name="personInformation"></param>
        /// <returns></returns>
        public Task<Guid[]> CreateAsync(PersonInformationEntity personInformation);

        /// <summary>
        /// Updates personal information about existed user by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personInformation"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, PersonInformationEntity personInformation);

        /// <summary>
        ///  Deletes user and information about this user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
