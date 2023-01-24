using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class AirlineGetAllQueryHandler : IRequestHandler<AirlineGetAllQuery, List<AirlineDto>>
    {
        private readonly IAirlineService _airlineService;

        public AirlineGetAllQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<List<AirlineDto>> Handle(AirlineGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetAllAsync();
        }
    }
}
