using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirplaneCreateCommandHandler : IRequestHandler<AirplaneCreateCommand, Guid>
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneCreateCommandHandler(IAirplaneService airplaneService) => _airplaneService = airplaneService;

        public async Task<Guid> Handle(AirplaneCreateCommand request, CancellationToken cancellationToken)
        {
            return await _airplaneService.CreateAsync(request.AirplaneDto);
        }
    }
}
