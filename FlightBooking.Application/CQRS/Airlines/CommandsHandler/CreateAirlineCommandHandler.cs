using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class CreateAirlineCommandHandler : IRequestHandler<CreateAirlineCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public CreateAirlineCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(CreateAirlineCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.CreateAsync(request.AirlineDto);
        }
    }
}
