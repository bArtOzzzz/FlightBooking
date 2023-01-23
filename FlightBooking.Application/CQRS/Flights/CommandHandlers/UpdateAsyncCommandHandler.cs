using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.CommandHandlers
{
    public class UpdateAsyncCommandHandler : IRequestHandler<UpdateAsyncCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public UpdateAsyncCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(UpdateAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateAsync(request.Id, request.FlightDto);
        }
    }
}
