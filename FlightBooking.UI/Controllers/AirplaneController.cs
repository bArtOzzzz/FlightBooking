using FlightBooking.Application.CQRS.Commands;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.API.Models.Response;
using FlightBooking.API.Models.Request;
using FlightBooking.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using AutoMapper;
using MediatR;

namespace FlightBooking.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirplaneController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IValidator<AirplaneCreateOrUpdateRequest> _validator;
        private readonly ILogger<AirplaneController> _logger;

        public AirplaneController(IMediator mediator,
                                 IMapper mapper,
                                 IValidator<AirplaneCreateOrUpdateRequest> validator,
                                 ILogger<AirplaneController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between AirplaneResponse and AirplaneDto
        /// Sends request by MediatR to database for getting all notes about airplanes
        /// Returns list of airplanes
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var airplanesMap = _mapper.Map<List<AirplaneResponse>>(await _mediator.Send(new AirplaneGetAllQuery()));

            if (!airplanesMap.Any())
            {
                _logger.LogError("ERROR 404 [AirplaneController (GetAllAsync)]: An error occurred while getting the list of airplanes");
                return NotFound();
            }

            return Ok(airplanesMap);
        }

        /// <summary>
        /// Creates mapping between AirplaneResponse and AirplaneDto
        /// Sends request by MediatR to database for getting airplane by id
        /// Returns airplane
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var airplaneMap = _mapper.Map<AirplaneResponse>(await _mediator.Send(new AirplaneGetByIdQuery(id)));

            if (airplaneMap == null)
            {
                _logger.LogError("ERROR 404 [AirplaneController (GetByIdAsync)]: An error occurred while getting the airplane by id");
                return NotFound();
            }

            return Ok(airplaneMap);
        }

        /// <summary>
        /// Creates mapping between AirplaneCreateOrUpdateRequest and AirplaneDto
        /// Sends request by MediatR to database for create new airplane
        /// Returns the id of the created airplane
        /// </summary>
        /// <param name="airplaneCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(AirplaneCreateOrUpdateRequest airplaneCreateOrUpdate)
        {
            if (airplaneCreateOrUpdate == null)
            {
                _logger.LogError("ERROR 404 [AirplaneController (CreateAsync)]: An error occurred while create the airplane");
                return NotFound();
            }

            await _validator.ValidateAndThrowAsync(airplaneCreateOrUpdate);

            var airplaneMap = _mapper.Map<AirplaneDto>(airplaneCreateOrUpdate);

            return Ok(await _mediator.Send(new AirplaneCreateCommand(airplaneMap), default));
        }

        /// <summary>
        /// Creates mapping between AirplaneCreateOrUpdateRequest and AirplaneDto
        /// Sends request by MediatR to database for update airplane
        /// Returns the id of the updated airplane
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airplaneCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, AirplaneCreateOrUpdateRequest airplaneCreateOrUpdate)
        {
            await _validator.ValidateAndThrowAsync(airplaneCreateOrUpdate);

            if (airplaneCreateOrUpdate == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [AirplaneController (UpdateAsync)]: An error occurred while update the airplane");
                return NotFound();
            }

            var airplaneMap = _mapper.Map<AirplaneDto>(airplaneCreateOrUpdate);

            return Ok(await _mediator.Send(new AirplaneUpdateCommand(id, airplaneMap)));
        }

        /// <summary>
        /// Sends request by MediatR to database for delete airplane
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [AirplaneController (DeleteAsync)]: An error occurred while delete the airplane");
                return NotFound();
            }

            await _mediator.Send(new AirplaneDeleteCommand(id));

            return NoContent();
        }
    }
}
