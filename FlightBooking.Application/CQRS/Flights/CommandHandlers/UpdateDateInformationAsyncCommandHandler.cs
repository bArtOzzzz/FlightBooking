using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Flights.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.CommandHandlers
{
    public class UpdateDateInformationAsyncCommandHandler : IRequestHandler<UpdateDateInformationAsyncCommand, Guid>
    {
        private readonly IFlightService _flightService;

        public UpdateDateInformationAsyncCommandHandler(IFlightService flightService) => _flightService = flightService;

        public async Task<Guid> Handle(UpdateDateInformationAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _flightService.UpdateDateInformationAsync(request.Id, request.FlightDto);
        }
    }
}
