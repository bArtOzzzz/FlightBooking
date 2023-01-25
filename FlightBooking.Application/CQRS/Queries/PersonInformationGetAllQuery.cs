using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Queries
{
    public record class PersonInformationGetAllQuery : IRequest<List<PersonInformationDto>>;
}
