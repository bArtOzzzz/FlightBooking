using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record AirlineGetAllQuery : IRequest<List<AirlineDto>>;
}
