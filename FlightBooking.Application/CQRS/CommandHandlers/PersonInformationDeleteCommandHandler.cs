using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class PersonInformationDeleteCommandHandler : IRequestHandler<PersonInformationDeleteCommand, bool>
    {
        private readonly IPersonInformationService _personInformationService;

        public PersonInformationDeleteCommandHandler(IPersonInformationService personInformationService) => _personInformationService = personInformationService;

        public async Task<bool> Handle(PersonInformationDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _personInformationService.DeleteAsync(request.id);
        }
    }
}
