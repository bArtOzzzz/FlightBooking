using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.CommandHandlers
{
    public class CreateAsyncCommandHandler : IRequestHandler<CreateAsyncCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public CreateAsyncCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(CreateAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.CreateAsync(request.FlightDto);
        }
    }
}
