using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlightBooking.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Application.CQRS.Queries.GetAllQuery
{
    public class AirlineListQueryhandler : IRequestHandler<AirlineListQuery, AirlineListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AirlineListQueryhandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AirlineListVm> Handle(AirlineListQuery request, CancellationToken cancellationToken)
        {
            var airlineQuery = await _context.Airlines.Where(airline => airline.Id == request.Id)
                                                      .ProjectTo<AirlineDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

            return new AirlineListVm { Airlines = airlineQuery };
        }
    }
}
