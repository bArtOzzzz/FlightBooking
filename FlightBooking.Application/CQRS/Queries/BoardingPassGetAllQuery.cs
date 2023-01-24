using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record class BoardingPassGetAllQuery : IRequest<List<BoardingPassDto>>;
}
