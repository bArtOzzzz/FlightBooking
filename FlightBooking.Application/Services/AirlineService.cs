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

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Gets all airlines data from repository
        /// </summary>
        /// <returns></returns>
        public async Task<List<AirlineDto>> GetAllAsync()
        {
            var airlines = await _airlineRepository.GetAllAsync();

            return _mapper.Map<List<AirlineDto>>(airlines);
        }

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Gets airline data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AirlineDto> GetByIdAsync(Guid id)
        {
            var airline = await _airlineRepository.GetByIdAsync(id);

            return _mapper.Map<AirlineDto>(airline);
        }

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Create new airline from repository
        /// </summary>
        /// <param name="airline"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAsync(AirlineDto airline)
        {
            var airlineMap = _mapper.Map<AirlineEntity>(airline);

            await _airlineRepository.CreateAsync(airlineMap);
            airline.Id = airlineMap.Id;
                
            return airlineMap.Id;
        }

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Updates airline data by id from repository
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airline"></param>
        /// <returns></returns>
        public async Task<Guid> UpdateAsync(Guid id, AirlineDto airline)
        {
            var airlineMap = _mapper.Map<AirlineEntity>(airline);

            return await _airlineRepository.UpdateAsync(id, airlineMap);
        }

        /// <summary>
        /// Creates mapping for data between AirlineDto and AirlineEntity
        /// Deletes airline from repository
        /// </summary>
        /// <param name="airlineEntity"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _airlineRepository.DeleteAsync(id);
        }
    }
}
