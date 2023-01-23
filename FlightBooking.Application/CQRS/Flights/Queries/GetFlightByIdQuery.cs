using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Queries
{
    public record class GetFlightByIdQuery(Guid id) : IRequest<FlightDto>;
}
