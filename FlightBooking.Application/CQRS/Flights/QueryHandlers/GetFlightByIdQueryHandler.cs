using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.QueryHandlers
{
    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDto>
    {
        private readonly IFlightService _flightService;

        public GetFlightByIdQueryHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<FlightDto> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetByIdAsync(request.id);
        }
    }
}
