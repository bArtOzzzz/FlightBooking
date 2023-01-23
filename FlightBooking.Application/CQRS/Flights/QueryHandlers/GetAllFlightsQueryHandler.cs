using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.QueryHandlers
{
    public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, List<FlightDto>>
    {
        private readonly IFlightService _flightService;

        public GetAllFlightsQueryHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<List<FlightDto>> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetAllAsync();
        }
    }
}
