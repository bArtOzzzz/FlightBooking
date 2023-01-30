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
    public class BoardingPassController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly IValidator<BoardingPassCreateRequest> _boardingPassCreateValidator;
        private readonly IValidator<BoardingPassUpdateRequest> _boardingPassUpdateValidator;

        private readonly ILogger<BoardingPassController> _logger;

        public BoardingPassController(IMediator mediator,
                                 IMapper mapper,
                                 IValidator<BoardingPassCreateRequest> boardingPassCreateValidator,
                                 IValidator<BoardingPassUpdateRequest> boardingPassUpdateValidator,
                                 ILogger<BoardingPassController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _boardingPassCreateValidator = boardingPassCreateValidator;
            _boardingPassUpdateValidator = boardingPassUpdateValidator;
            _logger = logger;
        }

        /// <summary>
        /// Creates mapping between BoardingPassResponse and BoardingPassDto
        /// Sends request by MediatR to database for getting all notes about boarding passes
        /// Returns list of boarding passes
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllAsync()
        {
            var boardingPassMap = _mapper.Map<List<BoardingPassResponse>>(await _mediator.Send(new BoardingPassGetAllQuery()));

            if (!boardingPassMap.Any())
            {
                _logger.LogError("ERROR 404 [BoardingPassController (GetAllAsync)]: An error occurred while getting the list of boarding passes");
                return NotFound();
            }

            return Ok(boardingPassMap);
        }

        /// <summary>
        /// Creates mapping between BoardingPassResponse and BoardingPassDto
        /// Sends request by MediatR to database for getting boarding pass by id
        /// Returns boarding pass
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            var airlineMap = _mapper.Map<BoardingPassResponse>(await _mediator.Send(new BoardingPassGetByIdQuery(id)));

            if (airlineMap == null)
            {
                _logger.LogError("ERROR 404 [BoardingPassController (GetByIdAsync)]: An error occurred while getting the boarding pass by id");
                return NotFound();
            }

            return Ok(airlineMap);
        }

        /// <summary>
        /// Creates mapping between BoardingPassCreateRequest and BoardingPassDto
        /// Sends request by MediatR to database for create new boarding pass
        /// Returns the id of the created boarding pass
        /// </summary>
        /// <param name="boardingPassCreateRequest"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(BoardingPassCreateRequest boardingPassCreateRequest)
        {
            if (boardingPassCreateRequest == null)
            {
                _logger.LogError("ERROR 404 [BoardingPassController (CreateAsync)]: An error occurred while create the boarding pass");
                return NotFound();
            }

            await _boardingPassCreateValidator.ValidateAndThrowAsync(boardingPassCreateRequest);

            var boardingPassMap = _mapper.Map<BoardingPassDto>(boardingPassCreateRequest);

            return Ok(await _mediator.Send(new BoardingPassCreateCommand(boardingPassMap), default));
        }

        /// <summary>
        /// Creates mapping between BoardingPassUpdateRequest and BoardingPassDto
        /// Sends request by MediatR to database for update boarding pass
        /// Returns the id of the updated boarding pass
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boardingPassUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, BoardingPassUpdateRequest boardingPassUpdateRequest)
        {
            await _boardingPassUpdateValidator.ValidateAndThrowAsync(boardingPassUpdateRequest);

            if (boardingPassUpdateRequest == null
                || id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [BoardingPassController (UpdateAsync)]: An error occurred while update the boarding pass");
                return NotFound();
            }

            var boardingPassMap = _mapper.Map<BoardingPassDto>(boardingPassUpdateRequest);

            return Ok(await _mediator.Send(new BoardingPassUpdateCommand(id, boardingPassMap)));
        }

        /// <summary>
        /// Sends request by MediatR to database for delete boarding pass
        /// Returns no content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                _logger.LogError("ERROR 404 [BoardingPassController (DeleteAsync)]: An error occurred while delete the boarding pass");
                return NotFound();
            }

            await _mediator.Send(new BoardingPassDeleteCommand(id));

            return NoContent();
        }
    }
}
