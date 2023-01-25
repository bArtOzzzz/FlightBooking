using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class PersonInformationUpdateCommandHandler : IRequestHandler<PersonInformationUpdateCommand, Guid>
    {
        private readonly IPersonInformationService _personInformationService;

        public PersonInformationUpdateCommandHandler(IPersonInformationService personInformationService) => _personInformationService = personInformationService;

        public async Task<Guid> Handle(PersonInformationUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _personInformationService.UpdateAsync(request.id, request.PersonInformationDto);
        }
    }
}
