using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class UpdateAsyncCommandHandler : IRequestHandler<UpdateAsyncCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public UpdateAsyncCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(UpdateAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.UpdateAsync(request.Id, request.AirlineDto);
        }
    }
}
