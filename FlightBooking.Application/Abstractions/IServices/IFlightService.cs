using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IFlightService
    {
        /// <summary>
        /// Creates mapping for data between FlightDto and FlightEntity
        /// Gets all Flights data from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<FlightDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between FlightDto and FlightEntity
        /// Gets flight data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<FlightDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates mapping for data between FlightDto and FlightEntity
        /// Create new flight from repository
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(FlightDto flight);

        /// <summary>
        /// Creates mapping for data between FlightDto and FlightEntity
        /// Updates flight data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, FlightDto flight);

        /// <summary>
        /// Updates flight departurer and arrival destination only by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> UpdateDescriptionAsync(Guid id, FlightDto flightDto);

        /// <summary>
        /// Updates only flight departurer date and arrival time by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Task<Guid> UpdateDateInformationAsync(Guid id, FlightDto flight);

        /// <summary>
        /// Deletes flight by id from repository
        /// </summary>
        /// <param name="airlineEntity"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
