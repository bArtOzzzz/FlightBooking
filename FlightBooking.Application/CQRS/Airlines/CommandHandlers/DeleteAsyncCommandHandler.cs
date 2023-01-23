using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class DeleteAsyncCommandHandler : IRequestHandler<DeleteAsyncCommand, bool>
    {
        private readonly IAirlineService _airlineService;

        public DeleteAsyncCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<bool> Handle(DeleteAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.DeleteAsync(request.Id);
        }
    }
}
