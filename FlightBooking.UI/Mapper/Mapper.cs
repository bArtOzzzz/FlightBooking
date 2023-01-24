using FlightBooking.API.Models.Response;
using FlightBooking.API.Models.Request;
using FlightBooking.Application.Dto;
using FlightBooking.Domain.Entities;
using AutoMapper;

namespace FlightBooking.Application.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<AirlineEntity, AirlineDto>();
            CreateMap<AirlineDto, AirlineEntity>();

            CreateMap<AirlineDto, AirlineResponse>();
            CreateMap<AirlineResponse, AirlineDto>();

            CreateMap<AirlineCreateOrUpdateRequest, AirlineDto>();
            CreateMap<AirlineDto, AirlineCreateOrUpdateRequest>();


            CreateMap<FlightEntity, FlightDto>();
            CreateMap<FlightDto, FlightEntity>();

            CreateMap<FlightDto, FlightResponse>();
            CreateMap<FlightResponse, FlightDto>();

            CreateMap<FlightCreateOrUpdateRequest, FlightDto>();
            CreateMap<FlightDto, FlightCreateOrUpdateRequest>();

            CreateMap<FlightUpdateDescriptionRequest, FlightDto>();
            CreateMap<FlightDto, FlightUpdateDescriptionRequest>();

            CreateMap<FlightUpdateDateInformationRequest, FlightDto>();
            CreateMap<FlightDto, FlightUpdateDateInformationRequest>();


            CreateMap<AirplaneEntity, AirplaneDto>();
            CreateMap<AirplaneDto, AirplaneEntity>();
        }
    }
}
