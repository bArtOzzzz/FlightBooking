using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Flights.Queries
{
    public record GetAllFlightsQuery : IRequest<List<FlightDto>>;
}
