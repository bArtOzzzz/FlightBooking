using FlightBooking.Application.Abstractions;
using FlightBooking.Application.Common.Exceptions;
using FlightBooking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Application.CQRS.Commands.UpdateCommands
{
    public class UpdateAirlineCommandHandler : IRequestHandler<UpdateAirlineCommand>
    {
        private readonly IApplicationDbContext _context;
         
        public UpdateAirlineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAirlineCommand request, CancellationToken token)
        {
            var entity = await _context.Airlines.FirstOrDefaultAsync(airline => airline.Id.Equals(request.Id), token);

            if (entity == null)
            {
                throw new NotFoundException(nameof(AirlineEntity), request.Id);
            }

            entity.AirlineName = request.AirlineName;
            entity.Rating = request.Rating; 

            await _context.SaveChangesAsync(token);

            return Unit.Value;
        }
    }
}
