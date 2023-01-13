using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class UpdateAirlineCommandHandler : IRequestHandler<UpdateAirlineCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public UpdateAirlineCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(UpdateAirlineCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.UpdateAsync(request.Id, request.AirlineDto);
        }
    }
}
