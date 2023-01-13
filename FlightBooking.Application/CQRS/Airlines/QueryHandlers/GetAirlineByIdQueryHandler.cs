using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.QueryHandlers
{
    public class GetAirlineByIdQueryHandler : IRequestHandler<GetAirlineByIdQuery, AirlineDto>
    {
        private readonly IAirlineService _airlineService;

        public GetAirlineByIdQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<AirlineDto> Handle(GetAirlineByIdQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetByIdAsync(request.id);
        }
    }
}
