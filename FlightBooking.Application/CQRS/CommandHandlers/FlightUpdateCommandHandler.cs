using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class FlightUpdateCommandHandler : IRequestHandler<FlightUpdateCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public FlightUpdateCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(FlightUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateAsync(request.Id, request.FlightDto);
        }
    }
}
