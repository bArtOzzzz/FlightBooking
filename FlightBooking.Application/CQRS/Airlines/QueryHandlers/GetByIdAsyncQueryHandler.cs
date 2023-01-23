using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.QueryHandlers
{
    public class GetByIdAsyncQueryHandler : IRequestHandler<GetByIdAsyncQuery, AirlineDto>
    {
        private readonly IAirlineService _airlineService;

        public GetByIdAsyncQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<AirlineDto> Handle(GetByIdAsyncQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetByIdAsync(request.id);
        }
    }
}
