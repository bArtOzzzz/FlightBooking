using MediatR;

namespace FlightBooking.Application.CQRS.Queries.GetByIdQuery
{
    public class GetAirlineByIdQuery : IRequest<AirlineVm>
    {
        public Guid Id { get; set; }
    }
}
