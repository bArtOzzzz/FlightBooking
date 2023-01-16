using FlightBooking.Application.CQRS.Airlines.Commands;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.API.Models.Response;
using FlightBooking.API.Models.Request;
using FlightBooking.Application.Dto;
using Microsoft.AspNetCore.Mvc;
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

        public AirlineController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
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
            var airlinesResponse = await _mediator.Send(new GetAllAirlinesQuery());

            return Ok(_mapper.Map<List<AirlineResponse>>(airlinesResponse));
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
            var airlineResponse = await _mediator.Send(new GetAirlineByIdQuery(id));

            return Ok(_mapper.Map<AirlineResponse>(airlineResponse));
        }
        
        /// <summary>
        /// Creates mapping between AirlineResponse and AirlineDto
        /// Sends request by MediatR to database for create new airline
        /// Returns the id of the created airline
        /// </summary>
        /// <param name="airlineCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(AirlineCreateOrUpdate airlineCreateOrUpdate)
        {
            if(airlineCreateOrUpdate == null || !ModelState.IsValid)
                return NotFound();

            Guid airlineId = await _mediator.Send(new CreateAirlineCommand(_mapper.Map<AirlineDto>(airlineCreateOrUpdate)));

            return Ok(airlineId);
        }

        /// <summary>
        /// Creates mapping between AirlineResponse and AirlineDto
        /// Sends request by MediatR to database for update airline
        /// Returns the id of the updated airline
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airlineCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<ActionResult> UpdateAsync(Guid id, AirlineCreateOrUpdate airlineCreateOrUpdate)
        {
            if(airlineCreateOrUpdate == null || !ModelState.IsValid)
                return NotFound();

            await _mediator.Send(new UpdateAirlineCommand(id, _mapper.Map<AirlineDto>(airlineCreateOrUpdate)));

            return Ok(id);
        }

        /// <summary>
        /// Sends request by MediatR to database for delete airline
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteAirlineCommand(id));

            return NoContent();
        }
     }
}
