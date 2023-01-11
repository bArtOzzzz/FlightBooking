using AutoMapper;
using FlightBooking.Application.Common.Mapping;
using FlightBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.CQRS.Queries.GetByIdQuery
{
    public class AirlineVm : IMapWith<AirlineEntity>
    {
        public Guid Id { get; set; }
        public string? AirlineName { get; set; }
        public double Rating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AirlineEntity, AirlineVm>()
                .ForMember(airlineVm => airlineVm.AirlineName, opt => opt.MapFrom(airline => airline.AirlineName))
                .ForMember(airlineVm => airlineVm.Rating, opt => opt.MapFrom(airline => airline.Rating))
                .ForMember(airlineVm => airlineVm.Id, opt => opt.MapFrom(airline => airline.Id));
        }
    }
}
