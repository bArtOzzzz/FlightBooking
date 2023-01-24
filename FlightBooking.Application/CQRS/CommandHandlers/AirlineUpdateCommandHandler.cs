using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirlineUpdateCommandHandler : IRequestHandler<AirlineUpdateAsyncCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public AirlineUpdateCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(AirlineUpdateAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.UpdateAsync(request.Id, request.AirlineDto);
        }
    }
}
