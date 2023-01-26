﻿using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.Commands
{
    public record class PersonInformationCreateCommand(PersonInformationDto PersonInformationDto) : IRequest<Guid[]>;
}