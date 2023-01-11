using AutoMapper;
using FlightBooking.Application.Common.Mapping;
using FlightBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.CQRS.Queries.GetAllQuery
{
    public class AirlineDto : IMapWith<AirlineEntity>
    {
        public Guid Id { get; set; }
        public string? AirlineName { get; set; }
        public double Rating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AirlineEntity, AirlineDto>()
                .ForMember(airlineDto => airlineDto.Id, opt => opt.MapFrom(airline => airline.Id))
                .ForMember(airlineDto => airlineDto.AirlineName, opt => opt.MapFrom(airline => airline.AirlineName))
                .ForMember(airlineDto => airlineDto.Rating, opt => opt.MapFrom(airline => airline.Rating));
        }
    }
}
