using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepositories
{
    public interface IBoardingPassRepository
    {
        /// <summary>
        /// Gets all notes about boarding passes from database
        /// </summary>
        /// <returns></returns>
        public Task<List<BoardingPassEntity>> GetAllAsync();

        /// <summary>
        /// Gets boarding pass by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<BoardingPassEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates new note about boarding pass and saves in database
        /// </summary>
        /// <param name="boardingPass"></param>
        /// <returns></returns>
        public Task<Guid> CreateAsync(BoardingPassEntity boardingPass);

        /// <summary>
        /// Updates information about existed boarding pass by id and saves in database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boardingPass"></param>
        /// <returns></returns>
        public Task<Guid> UpdateAsync(Guid id, BoardingPassEntity boardingPass);

        /// <summary>
        /// Deletes all information about existed boarding pass from database and saves changes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}
