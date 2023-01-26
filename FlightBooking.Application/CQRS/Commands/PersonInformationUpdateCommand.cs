using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class PersonInformationUpdateCommand(Guid id, PersonInformationDto PersonInformationDto) : IRequest<Guid>;
}
