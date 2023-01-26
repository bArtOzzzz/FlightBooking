using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class FlightUpdateDateInformationCommand(Guid Id, FlightDto FlightDto) : IRequest<Guid>;
}
