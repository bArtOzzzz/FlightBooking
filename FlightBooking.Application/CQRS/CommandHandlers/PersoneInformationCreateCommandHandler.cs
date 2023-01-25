using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class PersoneInformationCreateCommandHandler : IRequestHandler<PersonInformationCreateCommand, Guid[]>
    {
        private readonly IPersonInformationService _personInformationService;

        public PersoneInformationCreateCommandHandler(IPersonInformationService personInformationService) => _personInformationService = personInformationService;

        public async Task<Guid[]> Handle(PersonInformationCreateCommand request, CancellationToken cancellationToken)
        {
            return await _personInformationService.CreateAsync(request.PersonInformationDto);
        }
    }
}
