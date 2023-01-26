using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class FlightGetAllQueryHandler : IRequestHandler<FlightGetAllQuery, List<FlightDto>>
    {
        private readonly IFlightService _flightService;

        public FlightGetAllQueryHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<List<FlightDto>> Handle(FlightGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _flightService.GetAllAsync();
        }
    }
}
