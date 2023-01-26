using FlightBooking.Application.Abstractions.IRepositories;
using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.Dto;
using AutoMapper;

namespace FlightBooking.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
