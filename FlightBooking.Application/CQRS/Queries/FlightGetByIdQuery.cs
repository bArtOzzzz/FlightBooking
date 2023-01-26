using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record class FlightGetByIdQuery(Guid id) : IRequest<FlightDto>;
}
