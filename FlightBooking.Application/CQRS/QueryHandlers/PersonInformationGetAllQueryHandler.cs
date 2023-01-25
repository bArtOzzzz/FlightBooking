using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class PersonInformationGetAllQueryHandler : IRequestHandler<PersonInformationGetAllQuery, List<PersonInformationDto>>
    {
        private readonly IPersonInformationService _personInformationService;

        public PersonInformationGetAllQueryHandler(IPersonInformationService personInformationService) => _personInformationService = personInformationService;

        public async Task<List<PersonInformationDto>> Handle(PersonInformationGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _personInformationService.GetAllAsync();
        }
    }
}
