using AutoMapper;
using FlightBooking.Application.Abstractions;
using FlightBooking.Application.Common.Exceptions;
using FlightBooking.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Application.CQRS.Queries.GetByIdQuery
{
    public class GetAirlineByIdQueryHandler : IRequestHandler<GetAirlineByIdQuery, AirlineVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAirlineByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirlineVm> Handle(GetAirlineByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Airlines.FirstOrDefaultAsync(airline => airline.Id == request.Id, cancellationToken);

            if(entity == null)
            {
                throw new NotFoundException(nameof(AirlineEntity), request.Id);
            }

            return _mapper.Map<AirlineVm>(entity);
        }
    }
}
