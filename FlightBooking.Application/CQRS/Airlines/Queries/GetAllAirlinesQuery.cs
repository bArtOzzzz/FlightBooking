using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Queries
{
    public record GetAllAirlinesQuery : IRequest<List<AirlineDto>>;
}
