using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class FlightUpdateDescriptionCommandHandler : IRequestHandler<FlightUpdateDescriptionCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public FlightUpdateDescriptionCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(FlightUpdateDescriptionCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateDescriptionAsync(request.Id, request.FlightDto);
        }
    }
}
