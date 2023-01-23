using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Airlines.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.CommandsHandler
{
    public class CreateAsyncCommandHandler : IRequestHandler<CreateAsyncCommand, Guid>
    {
        private readonly IAirlineService _airlineService;

        public CreateAsyncCommandHandler(IAirlineService airlineService) => _airlineService = airlineService;

        public async Task<Guid> Handle(CreateAsyncCommand request, CancellationToken cancellationToken)
        {
            return await _airlineService.CreateAsync(request.AirlineDto);
        }
    }
}
