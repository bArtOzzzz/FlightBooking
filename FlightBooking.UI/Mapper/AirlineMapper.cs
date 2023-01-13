using FlightBooking.API.Models.Response;
using FlightBooking.API.Models.Request;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;
using AutoMapper;

namespace FlightBooking.Application.Mapper
{
    public class AirlineMapper : Profile
    {
        public AirlineMapper()
        {
            CreateMap<AirlineEntity, AirlineDto>();
            CreateMap<AirlineDto, AirlineEntity>();

            CreateMap<AirlineDto, AirlineResponse>();
            CreateMap<AirlineResponse, AirlineDto>();

            CreateMap<AirlineCreateOrUpdate, AirlineDto>();
            CreateMap<AirlineDto, AirlineCreateOrUpdate>();
        }
    }
}
