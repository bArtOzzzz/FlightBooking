using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IUserService
    {
        /// <summary>
        /// Creates mapping for data between UserDto and UserEntity
        /// Gets all users from repository
        /// </summary>
        /// <returns></returns>
        public Task<List<UserDto>> GetAllAsync();

        /// <summary>
        /// Creates mapping for data between UserDto and UserEntity
        /// Gets airline data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UserDto> GetByIdAsync(Guid id);
    }
}
