using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class FlightGetByIdQueryHandler : IRequestHandler<FlightGetByIdQuery, FlightDto>
    {
        private readonly IFlightService _flightService;

        public FlightGetByIdQueryHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<FlightDto> Handle(FlightGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetByIdAsync(request.id);
        }
    }
}
