using FlightBooking.Application.Abstractions.IRepository;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;
using AutoMapper;

namespace FlightBooking.Application.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public AirlineService(IAirlineRepository airlineRepository, IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public async Task<List<AirlineDto>> GetAllAsync()
        {
            var airlines = await _airlineRepository.GetAllAsync();

            return _mapper.Map<List<AirlineDto>>(airlines);
        }

        public async Task<AirlineDto> GetByIdAsync(Guid id)
        {
            var airline = await _airlineRepository.GetByIdAsync(id);

            return _mapper.Map<AirlineDto>(airline);
        }

        public async Task<Guid> CreateAsync(AirlineDto airline)
        {
            var airlineMap = _mapper.Map<AirlineEntity>(airline);

            return await _airlineRepository.CreateAsync(airlineMap);
        }

        public async Task<Guid> UpdateAsync(Guid id, AirlineDto airline)
        {
            var airlineMap = _mapper.Map<AirlineEntity>(airline);

            return await _airlineRepository.UpdateAsync(id, airlineMap);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _airlineRepository.DeleteAsync(id);
        }
    }
}
