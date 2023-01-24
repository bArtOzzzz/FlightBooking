using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class AirplaneGetByIdQueryHandler : IRequestHandler<AirplaneGetByIdQuery, AirplaneDto>
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneGetByIdQueryHandler(IAirplaneService airplaneService) => _airplaneService = airplaneService;

        public async Task<AirplaneDto> Handle(AirplaneGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _airplaneService.GetByIdAsync(request.id);
        }
    }
}
