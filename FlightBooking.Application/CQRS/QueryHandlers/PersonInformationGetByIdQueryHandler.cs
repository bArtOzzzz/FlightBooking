using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class PersonInformationGetByIdQueryHandler : IRequestHandler<PersonInformationGetByIdQuery, PersonInformationDto>
    {
        private readonly IPersonInformationService _personInformationService;

        public PersonInformationGetByIdQueryHandler(IPersonInformationService personInformationService) => _personInformationService = personInformationService;

        public async Task<PersonInformationDto> Handle(PersonInformationGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _personInformationService.GetByIdAsync(request.id);
        }
    }
}
