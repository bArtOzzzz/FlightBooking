using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Abstractions.IRepository
{
    public interface IAirlineRepository
    {
        public Task<List<AirlineEntity>> GetAllAsync();
        public Task<AirlineEntity?> GetByIdAsync(Guid id);
        public Task<Guid> CreateAsync(AirlineEntity airline);
        public Task<Guid> UpdateAsync(Guid id, AirlineEntity airline);
        public Task<bool> DeleteAsync(Guid id);
    }
}
