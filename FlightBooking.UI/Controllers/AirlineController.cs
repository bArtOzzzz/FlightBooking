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
    public class AirlineController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IValidator<AirlineCreateOrUpdateRequest> _validator;
        private readonly ILogger<AirlineController> _logger;

        public AirlineController(IMediator mediator, 
                                 IMapper mapper, 
                                 IValidator<AirlineCreateOrUpdateRequest> validator,
                                 ILogger<AirlineController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between AirlineResponse and AirlineDto
        /// Sends request by MediatR to database for getting all notes about airlines
        /// Returns list of airlines
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var airlinesMap = _mapper.Map<List<AirlineResponse>>(await _mediator.Send(new AirlineGetAllQuery()));

            if (!airlinesMap.Any())
            {
                _logger.LogError("ERROR 404 [AirlineController (GetAllAsync)]: An error occurred while getting the list of airlines");
                return NotFound();
            }

            return Ok(airlinesMap);
        }

        /// <summary>
        /// Creates mapping between AirlineResponse and AirlineDto
        /// Sends request by MediatR to database for getting airline by id
        /// Returns airline
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var airlineMap = _mapper.Map<AirlineResponse>(await _mediator.Send(new AirlineGetByIdQuery(id)));

            if (airlineMap == null)
            {
                _logger.LogError("ERROR 404 [AirlineController (GetByIdAsync)]: An error occurred while getting the airline by id");
                return NotFound();
            }

            return Ok(airlineMap);
        }

        /// <summary>
        /// Creates mapping between AirlineCreateOrUpdateRequest and AirlineDto
        /// Sends request by MediatR to database for create new airline
        /// Returns the id of the created airline
        /// </summary>
        /// <param name="airlineCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(AirlineCreateOrUpdateRequest airlineCreateOrUpdate)
        {
            if (airlineCreateOrUpdate == null)
            {
                _logger.LogError("ERROR 404 [AirlineController (CreateAsync)]: An error occurred while create airline");
                return NotFound();
            }

            await _validator.ValidateAndThrowAsync(airlineCreateOrUpdate);

            var airlineMap = _mapper.Map<AirlineDto>(airlineCreateOrUpdate);

            return Ok(await _mediator.Send(new AirlineCreateCommand(airlineMap), default));
        }

        /// <summary>
        /// Creates mapping between AirlineCreateOrUpdateRequest and AirlineDto
        /// Sends request by MediatR to database for update airline
        /// Returns the id of the updated airline
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airlineCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, AirlineCreateOrUpdateRequest airlineCreateOrUpdate)
        {
            await _validator.ValidateAndThrowAsync(airlineCreateOrUpdate);

            if (airlineCreateOrUpdate == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [AirlineController (UpdateAsync)]: An error occurred while update airline");
                return NotFound();
            }

            var airlineMap = _mapper.Map<AirlineDto>(airlineCreateOrUpdate);

            return Ok(await _mediator.Send(new AirlineUpdateCommand(id, airlineMap)));
        }

        /// <summary>
        /// Sends request by MediatR to database for delete airline
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if(id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [AirlineController (DeleteAsync)]: An error occurred while delete airline");
                return NotFound();
            }

            await _mediator.Send(new AirlineDeleteCommand(id));

            return NoContent();
        }
     }
}
