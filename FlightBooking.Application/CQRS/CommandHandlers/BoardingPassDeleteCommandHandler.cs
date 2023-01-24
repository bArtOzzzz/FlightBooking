using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class BoardingPassDeleteCommandHandler : IRequestHandler<BoardingPassDeleteCommand, bool>
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassDeleteCommandHandler(IBoardingPassService boardingPassService) => _boardingPassService = boardingPassService;

        public async Task<bool> Handle(BoardingPassDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _boardingPassService.DeleteAsync(request.id);
        }
    }
}
