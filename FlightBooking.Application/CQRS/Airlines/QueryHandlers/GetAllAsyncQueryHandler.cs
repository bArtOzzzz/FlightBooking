using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.QueryHandlers
{
    public class GetAllAsyncQueryHandler : IRequestHandler<GetAllAsyncQuery, List<AirlineDto>>
    {
        private readonly IAirlineService _airlineService;

        public GetAllAsyncQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<List<AirlineDto>> Handle(GetAllAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetAllAsync();
        }
    }
}
