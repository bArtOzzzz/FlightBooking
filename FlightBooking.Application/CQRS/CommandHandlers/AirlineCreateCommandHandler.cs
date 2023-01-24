using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class AirlineCreateCommandHandler : IRequestHandler<AirlineCreateCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public AirlineCreateCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(AirlineCreateCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.CreateAsync(request.AirlineDto);
        }
    }
}
