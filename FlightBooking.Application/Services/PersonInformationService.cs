using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using AutoMapper;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Services
{
    public class PersonInformationService : IPersonInformationService
    {
        private readonly IPersonInformationRepository _personInformationRepository;
        private readonly IMapper _mapper;

        public PersonInformationService(IPersonInformationRepository personInformationRepository, IMapper mapper)
        {
            _personInformationRepository = personInformationRepository;
            _mapper = mapper;
        }

        public async Task<List<PersonInformationDto>> GetAllAsync()
        {
            var personsInformation = await _personInformationRepository.GetAllAsync();

            return _mapper.Map<List<PersonInformationDto>>(personsInformation);
        }

        public async Task<PersonInformationDto> GetByIdAsync(Guid id)
        {
            var personInformation = await _personInformationRepository.GetByIdAsync(id);

            return _mapper.Map<PersonInformationDto>(personInformation);
        }

        public async Task<Guid[]> CreateAsync(PersonInformationDto personInformation)
        {
            var personInformationMap = _mapper.Map<PersonInformationEntity>(personInformation);

            return await _personInformationRepository.CreateAsync(personInformationMap);
        }

        public async Task<Guid> UpdateAsync(Guid id, PersonInformationDto personInformation)
        {
            var personInformationMap = _mapper.Map<PersonInformationEntity>(personInformation);

            return await _personInformationRepository.UpdateAsync(id, personInformationMap);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _personInformationRepository.DeleteAsync(id);
        }
    }
}
