using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Queries
{
    public record GetByIdAsyncQuery(Guid id) : IRequest<AirlineDto>;
}
