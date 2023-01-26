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

        public AirplaneController(IMediator mediator,
                                 IMapper mapper,
                                 IValidator<AirplaneCreateOrUpdateRequest> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
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
                return NotFound();

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
                return NotFound();

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
                return NotFound();

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
                return NotFound();

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
                return NotFound();

            await _mediator.Send(new AirplaneDeleteCommand(id));

            return NoContent();
        }
    }
}
