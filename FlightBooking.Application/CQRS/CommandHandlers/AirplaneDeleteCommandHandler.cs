using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirplaneDeleteCommandHandler : IRequestHandler<AirplaneDeleteCommand, bool>
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneDeleteCommandHandler(IAirplaneService airplaneService) => _airplaneService = airplaneService;

        public async Task<bool> Handle(AirplaneDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _airplaneService.DeleteAsync(request.id);
        }
    }
}
