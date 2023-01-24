using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IBoardingPassService
    {
        /// <summary>
        /// Creates mapping for data between BoardingPassDto and AirplaneEntity
        /// Gets all boarding passes data from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<BoardingPassDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between BoardingPassDto and AirplaneEntity
        /// Gets boarding pass data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BoardingPassDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates mapping for data between BoardingPassDto and AirplaneEntity
        /// Create new boarding pass from repository
        /// </summary>
        /// <param name="boardingPass"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(BoardingPassDto boardingPass);

        /// <summary>
        /// Creates mapping for data between BoardingPassDto and AirplaneEntity
        /// Updates boarding pass data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boardingPass"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, BoardingPassDto boardingPass);

        /// <summary>
        /// Creates mapping for data between BoardingPassDto and AirplaneEntity
        /// Deletes boarding pass from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
