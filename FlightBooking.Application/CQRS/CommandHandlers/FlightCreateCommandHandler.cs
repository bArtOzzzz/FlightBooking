using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class FlightCreateCommandHandler : IRequestHandler<FlightCreateCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public FlightCreateCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(FlightCreateCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.CreateAsync(request.FlightDto);
        }
    }
}
