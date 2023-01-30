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
    public class PersonInformationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IValidator<PersonInformationCreateOrUpdateRequest> _validator;

        private readonly ILogger<PersonInformationController> _logger;

        public PersonInformationController(IMediator mediator,
                                          IMapper mapper,
                                          IValidator<PersonInformationCreateOrUpdateRequest> validator,
                                          ILogger<PersonInformationController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between PersonInformationResponse and PersonInformationDto
        /// Sends request by MediatR to database for getting all notes about persons information
        /// Returns list of persons information
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var personInformationMap = _mapper.Map<List<PersonInformationResponse>>(await _mediator.Send(new PersonInformationGetAllQuery()));

            if (!personInformationMap.Any())
            {
                _logger.LogError("ERROR 404 [PersonInformationController (GetAllAsync)]: An error occurred while getting the list of persons informations");
                return NotFound();
            }

            return Ok(personInformationMap);
        }

        /// <summary>
        /// Creates mapping between PersonInformationResponse and PersonInformationDto
        /// Sends request by MediatR to database for getting person information by id
        /// Returns person information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var personInformationMap = _mapper.Map<PersonInformationResponse>(await _mediator.Send(new PersonInformationGetByIdQuery(id)));

            if (personInformationMap == null)
            {
                _logger.LogError("ERROR 404 [PersonInformationController (GetByIdAsync)]: An error occurred while getting persons information by id");
                return NotFound();
            }

            return Ok(personInformationMap);
        }

        /// <summary>
        /// Creates mapping between PersonInformationCreateOrUpdateRequest and PersonInformationDto
        /// Sends request by MediatR to database for create new person information and user
        /// Returns the id of the created user and personal information
        /// </summary>
        /// <param name="personInformationCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(PersonInformationCreateOrUpdateRequest personInformationCreateOrUpdate)
        {
            if (personInformationCreateOrUpdate == null)
            {
                _logger.LogError("ERROR 404 [PersonInformationController (CreateAsync)]: An error occurred while cretae persons information");
                return NotFound();
            }

            await _validator.ValidateAndThrowAsync(personInformationCreateOrUpdate);

            var personInformationMap = _mapper.Map<PersonInformationDto>(personInformationCreateOrUpdate);

            return Ok(await _mediator.Send(new PersonInformationCreateCommand(personInformationMap), default));
        }

        /// <summary>
        /// Creates mapping between PersonInformationCreateOrUpdateRequest and PersonInformationDto
        /// Sends request by MediatR to database for update person information
        /// Returns the id of the updated person information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personInformationCreateOrUpdate"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, PersonInformationCreateOrUpdateRequest personInformationCreateOrUpdate)
        {
            await _validator.ValidateAndThrowAsync(personInformationCreateOrUpdate);

            if (personInformationCreateOrUpdate == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [PersonInformationController (UpdateAsync)]: An error occurred while update persons information");
                return NotFound();
            }

            var personInformationMap = _mapper.Map<PersonInformationDto>(personInformationCreateOrUpdate);

            return Ok(await _mediator.Send(new PersonInformationUpdateCommand(id, personInformationMap)));
        }

        /// <summary>
        /// Sends request by MediatR to database for delete person information and user
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [PersonInformationController (DeleteAsync)]: An error occurred while delete persons information");
                return NotFound();
            }

            await _mediator.Send(new PersonInformationDeleteCommand(id));

            return NoContent();
        }
    }
}
