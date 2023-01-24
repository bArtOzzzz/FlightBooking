using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IAirplaneService
    {
        /// <summary>
        /// Creates mapping for data between AirplaneDto and AirplaneEntity
        /// Gets all airplanes data from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<AirplaneDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between AirplaneDto and AirplaneEntity
        /// Gets airplane data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<AirplaneDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates mapping for data between AirplaneDto and AirplaneEntity
        /// Create new airplane from repository
        /// </summary>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(AirplaneDto airline);

        /// <summary>
        /// Creates mapping for data between AirplaneDto and AirplaneEntity
        /// Updates airplane data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airline"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, AirplaneDto airline);

        /// <summary>
        /// Creates mapping for data between AirplaneDto and AirplaneEntity
        /// Deletes airplane from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
