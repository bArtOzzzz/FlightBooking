using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserDto>
    {
        private readonly IUserService _userService;

        public UserGetByIdQueryHandler(IUserService userService) => _userService = userService;

        public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsync(request.id);
        }
    }
}
