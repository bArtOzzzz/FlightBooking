using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.CQRS.Queries.GetAllQuery
{
    public class AirlineListVm
    {
        public IList<AirlineDto>? Airlines { get; set; }
    }
}
