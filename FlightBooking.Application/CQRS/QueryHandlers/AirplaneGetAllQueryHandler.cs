using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class AirplaneGetAllQueryHandler : IRequestHandler<AirplaneGetAllQuery, List<AirplaneDto>>
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneGetAllQueryHandler(IAirplaneService airplaneService) => _airplaneService = airplaneService;

        public async Task<List<AirplaneDto>> Handle(AirplaneGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _airplaneService.GetAllAsync();
        }
    }
}
