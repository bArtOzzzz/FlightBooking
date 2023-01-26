using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Domain.Entities;
using FlightBooking.Application.Dto;
using AutoMapper;

namespace FlightBooking.Application.Services
{
    public class BoardingPassService : IBoardingPassService
    {
        private readonly IBoardingPassRepository _boardingPassRepository;
        private readonly IMapper _mapper;

        public BoardingPassService(IBoardingPassRepository boardingPassRepository, IMapper mapper)
        {
            _boardingPassRepository = boardingPassRepository;
            _mapper = mapper;
        }

        public async Task<List<BoardingPassDto>> GetAllAsync()
        {
            var boardingPasses = await _boardingPassRepository.GetAllAsync();

            return _mapper.Map<List<BoardingPassDto>>(boardingPasses);
        }

        public async Task<BoardingPassDto> GetByIdAsync(Guid id)
        {
            var boardingPass = await _boardingPassRepository.GetByIdAsync(id);

            return _mapper.Map<BoardingPassDto>(boardingPass);
        }

        public async Task<Guid> CreateAsync(BoardingPassDto boardingPass)
        {
            var boardingPassEntity = _mapper.Map<BoardingPassEntity>(boardingPass);

            return await _boardingPassRepository.CreateAsync(boardingPassEntity);
        }

        public async Task<Guid> UpdateAsync(Guid id, BoardingPassDto boardingPass)
        {
            var boardingPassEntity = _mapper.Map<BoardingPassEntity>(boardingPass);

            return await _boardingPassRepository.UpdateAsync(id, boardingPassEntity);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            return _boardingPassRepository.DeleteAsync(id);
        }
    }
}
