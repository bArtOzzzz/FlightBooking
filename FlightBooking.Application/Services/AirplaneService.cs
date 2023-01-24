using AutoMapper;
using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Services
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IMapper _mapper;

        public AirplaneService(IAirplaneRepository airplaneRepository, IMapper mapper)
        {
            _airplaneRepository = airplaneRepository;
            _mapper = mapper;
        }

        public async Task<List<AirplaneDto>> GetAllAsync()
        {
            var airplanes = await _airplaneRepository.GetAllAsync();

            return _mapper.Map<List<AirplaneDto>>(airplanes);
        }

        public async Task<AirplaneDto> GetByIdAsync(Guid id)
        {
            var airplane = await _airplaneRepository.GetByIdAsync(id);

            return _mapper.Map<AirplaneDto>(airplane);
        }

        public async Task<Guid> CreateAsync(AirplaneDto airline)
        {
            var airplaneEntity = _mapper.Map<AirplaneEntity>(airline);

            return await _airplaneRepository.CreateAsync(airplaneEntity);
        }

        public async Task<Guid> UpdateAsync(Guid id, AirplaneDto airline)
        {
            var airplaneEntity = _mapper.Map<AirplaneEntity>(airline);

            return await _airplaneRepository.UpdateAsync(id, airplaneEntity);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _airplaneRepository.DeleteAsync(id);
        }
    }
}
