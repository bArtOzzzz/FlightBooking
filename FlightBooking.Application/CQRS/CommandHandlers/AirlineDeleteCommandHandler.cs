using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirlineDeleteCommandHandler : IRequestHandler<AirlineDeleteAsyncCommand, bool>
    {
        private readonly IAirlineService _airlineService;

        public AirlineDeleteCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<bool> Handle(AirlineDeleteAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.DeleteAsync(request.Id);
        }
    }
}
