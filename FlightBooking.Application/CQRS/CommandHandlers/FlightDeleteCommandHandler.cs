using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class FlightDeleteCommandHandler : IRequestHandler<FlightDeleteCommand, bool>
    {
        private readonly IFlightService _flightService;

        public FlightDeleteCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<bool> Handle(FlightDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.DeleteAsync(request.Id);
        }
    }
}
