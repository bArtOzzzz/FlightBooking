using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record AirlineGetByIdQuery(Guid id) : IRequest<AirlineDto>;
}
