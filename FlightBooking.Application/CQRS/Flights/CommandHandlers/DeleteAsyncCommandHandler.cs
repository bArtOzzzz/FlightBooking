using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.CommandHandlers
{
    public class DeleteAsyncCommandHandler : IRequestHandler<DeleteAsyncCommand, bool>
    {
        private readonly IFlightService _flightService;

        public DeleteAsyncCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<bool> Handle(DeleteAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.DeleteAsync(request.Id);
        }
    }
}
