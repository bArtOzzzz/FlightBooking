using FlightBooking.Application.Dto;

namespace FlightBooking.Application.Abstractions.IServices
{
    public interface IAirlineService
    {
        public Task<List<AirlineDto>> GetAllAsync();
        public Task<AirlineDto> GetByIdAsync(Guid id);
        public Task<Guid> CreateAsync(AirlineDto airline);
        public Task<Guid> UpdateAsync(Guid id, AirlineDto airline);
        public Task<bool> DeleteAsync(Guid id);
    }
}
