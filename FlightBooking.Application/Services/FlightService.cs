using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;
using AutoMapper;

namespace FlightBooking.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public async Task<List<FlightDto>> GetAllAsync()
        {
            var flights = await _flightRepository.GetAllAsync();

            return _mapper.Map<List<FlightDto>>(flights);
        }

        public async Task<FlightDto> GetByIdAsync(Guid id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);

            return _mapper.Map<FlightDto>(flight);
        }

        public async Task<Guid> CreateAsync(FlightDto flight)
        {
            var flightMap = _mapper.Map<FlightEntity>(flight);

            return await _flightRepository.CreateAsync(flightMap);
        }

        public async Task<Guid> UpdateAsync(Guid id, FlightDto flight)
        {
            var flightMap = _mapper.Map<FlightEntity>(flight);

            return await _flightRepository.UpdateAsync(id, flightMap);
        }

        public async Task<Guid> UpdateDescriptionAsync(Guid id, FlightDto flight)
        {
            var flightMap = _mapper.Map<FlightEntity>(flight);

            return await _flightRepository.UpdateDescriptionAsync(id, flightMap);
        }

        public async Task<Guid> UpdateDateInformationAsync(Guid id, FlightDto flight)
        {
            var flightMap = _mapper.Map<FlightEntity>(flight);

            return await _flightRepository.UpdateDateInformationAsync(id, flightMap);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _flightRepository.DeleteAsync(id);
        }
    }
}
