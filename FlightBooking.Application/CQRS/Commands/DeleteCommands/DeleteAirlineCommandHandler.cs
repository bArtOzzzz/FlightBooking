using FlightBooking.Application.Abstractions;
using FlightBooking.Application.Common.Exceptions;
using FlightBooking.Domain.Entities;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands.DeleteCommands
{
    public class DeleteAirlineCommandHandler : IRequestHandler<DeleteAirlineCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAirlineCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAirlineCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Airlines.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(AirlineEntity), request.Id);
            }

            _context.Airlines.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
