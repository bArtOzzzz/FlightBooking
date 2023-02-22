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
            #region Airlines
            CreateMap<AirlineEntity, AirlineDto>();
            CreateMap<AirlineDto, AirlineEntity>();

            CreateMap<AirlineDto, AirlineResponse>();
            CreateMap<AirlineResponse, AirlineDto>();

            CreateMap<AirlineCreateOrUpdateRequest, AirlineDto>();
            CreateMap<AirlineDto, AirlineCreateOrUpdateRequest>();
            #endregion

            #region Flights
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
            #endregion

            #region Airplane
            CreateMap<AirplaneEntity, AirplaneDto>();
            CreateMap<AirplaneDto, AirplaneEntity>();

            CreateMap<AirplaneDto, AirplaneResponse>();
            CreateMap<AirplaneResponse, AirplaneDto>();

            CreateMap<AirplaneCreateOrUpdateRequest, AirplaneDto>();
            CreateMap<AirplaneDto, AirplaneCreateOrUpdateRequest>();
            #endregion

            #region BoardingPass
            CreateMap<BoardingPassEntity, BoardingPassDto>();
            CreateMap<BoardingPassDto, BoardingPassEntity>();

            CreateMap<BoardingPassDto, BoardingPassResponse>();
            CreateMap<BoardingPassResponse, BoardingPassDto>();

            CreateMap<BoardingPassCreateRequest, BoardingPassDto>();
            CreateMap<BoardingPassDto, BoardingPassCreateRequest>();

            CreateMap<BoardingPassUpdateRequest, BoardingPassDto>();
            CreateMap<BoardingPassDto, BoardingPassUpdateRequest>();
            #endregion

            #region User
            CreateMap<UsersEntity, UserDto>();
            CreateMap<UserDto, UsersEntity>();

            CreateMap<UserDto, UserResponse>();
            CreateMap<UserResponse, UserDto>();
            #endregion

            #region PersonInformation
            CreateMap<PersonInformationEntity, PersonInformationDto>();
            CreateMap<PersonInformationDto, PersonInformationEntity>();

            CreateMap<PersonInformationDto, PersonInformationResponse>();
            CreateMap<PersonInformationResponse, PersonInformationDto>();

            CreateMap<PersonInformationDto, PersonInformationCreateOrUpdateRequest>();
            CreateMap<PersonInformationCreateOrUpdateRequest, PersonInformationDto>();
            #endregion

            #region others
            CreateMap<DateTime, string>();
            CreateMap<string, DateTime>();
            #endregion
        }
    }
}
