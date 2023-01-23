using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IAirlineService
    {
        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Gets all airlines data from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<AirlineDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Gets airline data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AirlineDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Create new airline from repository
        /// </summary>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(AirlineDto airline);

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Updates airline data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, AirlineDto airline);

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Deletes airline from repository
        /// </summary>
        /// <param name="airlineEntity"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
