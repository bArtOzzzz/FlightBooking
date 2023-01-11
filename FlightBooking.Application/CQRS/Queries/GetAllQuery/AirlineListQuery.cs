using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.CQRS.Queries.GetAllQuery
{
    public class AirlineListQuery : IRequest<AirlineListVm>
    {
        public Guid Id { get; set; }
    }
}
