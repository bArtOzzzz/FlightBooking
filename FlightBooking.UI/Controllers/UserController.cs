using AutoMapper;
using FlightBooking.API.Models.Response;
using FlightBooking.Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator,
                              IMapper mapper,
                              ILogger<UserController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between UserResponse and UserDto
        /// Sends request by MediatR to database for getting all notes about users
        /// Returns list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var usersMap = _mapper.Map<List<UserResponse>>(await _mediator.Send(new UserGetAllQuery()));

            if (!usersMap.Any())
            {
                _logger.LogError("ERROR 404 [UserController (GetAllAsync)]: An error occurred while getting the list of users");
                return NotFound();
            }

            return Ok(usersMap);
        }

        /// <summary>
        /// Creates mapping between UserResponse and UserDto
        /// Sends request by MediatR to database for getting user by id
        /// Returns user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var userMap = _mapper.Map<UserResponse>(await _mediator.Send(new UserGetByIdQuery(id)));

            if (userMap == null)
            {
                _logger.LogError("ERROR 404 [UserController (GetByIdAsync)]: An error occurred while getting user by id");
                return NotFound();
            }

            return Ok(userMap);
        }
    }
}
