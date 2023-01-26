using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class FlightUpdateDateInformationCommandHandler : IRequestHandler<FlightUpdateDateInformationCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public FlightUpdateDateInformationCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(FlightUpdateDateInformationCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateDateInformationAsync(request.Id, request.FlightDto);
        }
    }
}
