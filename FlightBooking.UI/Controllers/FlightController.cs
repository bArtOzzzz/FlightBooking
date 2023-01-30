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
    public class FlightController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IValidator<FlightCreateOrUpdateRequest> _validatorCreateOrUpdate;
        private readonly IValidator<FlightUpdateDescriptionRequest> _validatorUpdateDescription;
        private readonly IValidator<FlightUpdateDateInformationRequest> _validatorUpdateDateInformation;

        private readonly ILogger<FlightController> _logger;

        public FlightController(IMediator mediator,
                                IMapper mapper,
                                IValidator<FlightCreateOrUpdateRequest> validatorCreateOrUpdate,
                                IValidator<FlightUpdateDescriptionRequest> validatorUpdateDescription,
                                IValidator<FlightUpdateDateInformationRequest> validatorUpdateDateInformationRequest,
                                ILogger<FlightController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validatorCreateOrUpdate = validatorCreateOrUpdate;
            _validatorUpdateDescription = validatorUpdateDescription;
            _validatorUpdateDateInformation = validatorUpdateDateInformationRequest;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between FlightResponse and FlightDto
        /// Sends request by MediatR to database for getting all notes about flights
        /// Returns list of flights
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var flightMap = _mapper.Map<List<FlightResponse>>(await _mediator.Send(new FlightGetAllQuery()));

            if (!flightMap.Any())
            {
                _logger.LogError("ERROR 404 [FlightController (GetAllAsync)]: An error occurred while getting the list of flights");
                return NotFound();
            }

            return Ok(flightMap);
        }

        /// <summary>
        /// Creates mapping between FlightResponse and FlightDto
        /// Sends request by MediatR to database for getting flight by id
        /// Returns flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var flightMap = _mapper.Map<FlightResponse>(await _mediator.Send(new FlightGetByIdQuery(id)));

            if (flightMap == null)
            {
                _logger.LogError("ERROR 404 [FlightController (GetByIdAsync)]: An error occurred while getting the flight by id");
                return NotFound();
            }

            return Ok(flightMap);
        }

        /// <summary>
        /// Creates mapping between FlightCreateOrUpdateRequest and FlightDto
        /// Sends request by MediatR to database for create new flight
        /// Returns the id of the created flight
        /// </summary>
        /// <param name="flightCreateOrUpdateRequest"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(FlightCreateOrUpdateRequest flightCreateOrUpdateRequest)
        {
            if (flightCreateOrUpdateRequest == null)
            {
                _logger.LogError("ERROR 404 [FlightController (CreateAsync)]: An error occurred while create the flight");
                return NotFound();
            }

            await _validatorCreateOrUpdate.ValidateAndThrowAsync(flightCreateOrUpdateRequest);

            var flightMap = _mapper.Map<FlightDto>(flightCreateOrUpdateRequest);

            return Ok(await _mediator.Send(new FlightCreateCommand(flightMap), default));
        }

        /// <summary>
        /// Creates mapping between FlightCreateOrUpdateRequest and FlightDto
        /// Sends request by MediatR to database for update flight
        /// Returns the id of the updated flight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flightCreateOrUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, FlightCreateOrUpdateRequest flightCreateOrUpdateRequest)
        {
            await _validatorCreateOrUpdate.ValidateAndThrowAsync(flightCreateOrUpdateRequest);

            if (flightCreateOrUpdateRequest == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [FlightController (UpdateAsync)]: An error occurred while update the flight");
                return NotFound();
            }

            var flightMap = _mapper.Map<FlightDto>(flightCreateOrUpdateRequest);

            return Ok(await _mediator.Send(new FlightUpdateCommand(id, flightMap)));
        }

        /// <summary>
        /// Creates mapping between FlightUpdateDescriptionRequest and FlightDto
        /// Sends request by MediatR to database for update flight description
        /// Returns the id of the updated flight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flightUpdateDescriptionRequest"></param>
        /// <returns></returns>
        [HttpPut("UpdateDescription/{id}")]
        public async Task<ActionResult> UpdateDescriptionAsync(Guid id, FlightUpdateDescriptionRequest flightUpdateDescriptionRequest)
        {
            await _validatorUpdateDescription.ValidateAndThrowAsync(flightUpdateDescriptionRequest);

            if (flightUpdateDescriptionRequest == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [FlightController (UpdateDescriptionAsync)]: An error occurred while update description about flight");
                return NotFound();
            }

            var flightMap = _mapper.Map<FlightDto>(flightUpdateDescriptionRequest);

            return Ok(await _mediator.Send(new FlightUpdateDescriptionCommand(id, flightMap)));
        }

        /// <summary>
        /// Creates mapping between FlightUpdateDateInformationRequest and FlightDto
        /// Sends request by MediatR to database for update flight data information
        /// Returns the id of the updated flight
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flightUpdateDateInformationRequest"></param>
        /// <returns></returns>
        [HttpPut("UpdateDateInformation/{id}")]
        public async Task<ActionResult> UpdateDateInformationAsync(Guid id, FlightUpdateDateInformationRequest flightUpdateDateInformationRequest)
        {
            await _validatorUpdateDateInformation.ValidateAndThrowAsync(flightUpdateDateInformationRequest);

            if (flightUpdateDateInformationRequest == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [FlightController (UpdateDateInformationAsync)]: An error occurred while update date information about flight");
                return NotFound();
            }

            var flightMap = _mapper.Map<FlightDto>(flightUpdateDateInformationRequest);

            return Ok(await _mediator.Send(new FlightUpdateDateInformationCommand(id, flightMap)));
        }

        /// <summary>
        /// Sends request by MediatR to database for delete flight
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [FlightController (DeleteAsync)]: An error occurred while delete the information about flight");
                return NotFound();
            }

            await _mediator.Send(new FlightDeleteCommand(id));

            return NoContent();
        }
    }
}
