using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepositories
{
    public interface IFlightRepository
    {
        /// <summary>
        /// Gets all notes about flights from database
        /// </summary>
        /// <returns></returns>
        public Task<List<FlightEntity>> GetAllAsync();

        /// <summary>
        /// Gets flight by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<FlightEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates new note about flight and saves in database
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(FlightEntity flight);

        /// <summary>
        /// Updates information about existed flight by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, FlightEntity flight);

        /// <summary>
        /// Updates information about existed flight by id and saves in database
        /// Updates information about destination for flight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departurer"></param>
        /// <param name="arrival"></param>
        /// <returns></returns>
        public Task<Guid> UpdateDescriptionAsync(Guid id, FlightEntity flight);

        /// <summary>
        /// Updates information about existed flight by id and saves in database
        /// Updates information about arrivil and departure date
        /// </summary>
        /// <param name="id"></param>
        /// <param name="departurerDate"></param>
        /// <param name="arrivalTime"></param>
        /// <returns></returns>
        public Task<Guid> UpdateDateInformationAsync(Guid id, FlightEntity flight);

        /// <summary>
        /// Deletes all information about existed flight from database and saves changes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
