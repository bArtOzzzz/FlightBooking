﻿using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Airlines.Commands
{
    public record CreateAirlineCommand(AirlineDto AirlineDto) : IRequest<Guid>;
}
