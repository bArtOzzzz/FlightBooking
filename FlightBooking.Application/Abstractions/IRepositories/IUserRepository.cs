using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all notes about users from database
        /// </summary>
        /// <returns></returns>
        public Task<List<UsersEntity>> GetAllAsync();

        /// <summary>
        /// Gets user by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<UsersEntity?> GetByIdAsync(Guid id);
    }
}
