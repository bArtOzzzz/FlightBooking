using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepositories
{
    public interface IAirplaneRepository
    {
        /// <summary>
        /// Gets all notes about airplanes from database
        /// </summary>
        /// <returns></returns>
        public Task<List<AirplaneEntity>> GetAllAsync();

        /// <summary>
        /// Gets airplanes by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AirplaneEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates new note about airplane and saves in database
        /// </summary>
        /// <param name="airplane"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(AirplaneEntity airplane);

        /// <summary>
        /// Updates information about existed airplane by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airplane"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, AirplaneEntity airplane);

        /// <summary>
        /// Deletes all information about existed airplanes from database and saves changes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
