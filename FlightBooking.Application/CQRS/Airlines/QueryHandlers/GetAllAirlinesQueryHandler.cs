using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.QueryHandlers
{
    public class GetAllAirlinesQueryHandler : IRequestHandler<GetAllAirlinesQuery, List<AirlineDto>>
    {
        private readonly IAirlineService _airlineService;

        public GetAllAirlinesQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<List<AirlineDto>> Handle(GetAllAirlinesQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetAllAsync();
        }
    }
}
