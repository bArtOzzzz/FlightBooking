using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepository
{
    public interface IAirlineRepository
    {
        /// <summary>
        /// Gets all notes about airlines from database
        /// </summary>
        /// <returns></returns>
        public Task<List<AirlineEntity>> GetAllAsync();

        /// <summary>
        /// Gets airline by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AirlineEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates new note about airline and saves in database
        /// </summary>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(AirlineEntity airline);

        /// <summary>
        /// Updates information about existed airline by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, AirlineEntity airline);

        /// <summary>
        /// Deletes all information about existed airline from database and saves changes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
