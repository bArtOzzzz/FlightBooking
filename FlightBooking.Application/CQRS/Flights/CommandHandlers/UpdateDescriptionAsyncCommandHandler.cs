using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.CommandHandlers
{
    public class UpdateDescriptionAsyncCommandHandler : IRequestHandler<UpdateDescriptionAsyncCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public UpdateDescriptionAsyncCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(UpdateDescriptionAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateDescriptionAsync(request.Id, request.FlightDto);
        }
    }
}
