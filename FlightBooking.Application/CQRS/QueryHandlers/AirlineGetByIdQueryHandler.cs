using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class AirlineGetByIdQueryHandler : IRequestHandler<AirlineGetByIdQuery, AirlineDto>
    {
        private readonly IAirlineService _airlineService;

        public AirlineGetByIdQueryHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<AirlineDto> Handle(AirlineGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _airlineService.GetByIdAsync(request.id);
        }
    }
}
