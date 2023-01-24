using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record FlightGetAllQuery : IRequest<List<FlightDto>>;
}
