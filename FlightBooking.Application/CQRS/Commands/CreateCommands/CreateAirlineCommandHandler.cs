using FlightBooking.Application.Abstractions;
using FlightBooking.Domain.Entities;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands.CreateCommands
{
    public class CreateAirlineCommandHandler : IRequestHandler<CreateAirlineCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateAirlineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAirlineCommand request, CancellationToken token)
        {
            var airline = new AirlineEntity
            {
                Id = Guid.NewGuid(),
                AirlineName= request.AirlineName,
                Rating= request.Rating,
            };

            await _context.Airlines.AddAsync(airline);
            await _context.SaveChangesAsync(token);

            return airline.Id;
        }
    }
}
