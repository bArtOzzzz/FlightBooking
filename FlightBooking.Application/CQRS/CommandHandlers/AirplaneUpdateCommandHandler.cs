using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirplaneUpdateCommandHandler : IRequestHandler<AirplaneUpdateCommand, Guid>
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneUpdateCommandHandler(IAirplaneService airplaneService) => _airplaneService = airplaneService;

        public async Task<Guid> Handle(AirplaneUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _airplaneService.UpdateAsync(request.id, request.AirplaneDto);
        }
    }
}
