using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserDto>>
    {
        private readonly IUserService _userService;

        public UserGetAllQueryHandler(IUserService userService) => _userService = userService;

        public async Task<List<UserDto>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllAsync();
        }
    }
}
