using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class DeleteAirlineCommandHandler : IRequestHandler<DeleteAirlineCommand, bool>
    {
        private readonly IAirlineService _airlineService;

        public DeleteAirlineCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<bool> Handle(DeleteAirlineCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.DeleteAsync(request.Id);
        }
    }
}
