using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class BoardingPassCreateCommandHandler : IRequestHandler<BoardingPassCreateCommand, Guid>
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassCreateCommandHandler(IBoardingPassService boardingPassService) => _boardingPassService = boardingPassService;

        public async Task<Guid> Handle(BoardingPassCreateCommand request, CancellationToken cancellationToken)
        {
            return await _boardingPassService.CreateAsync(request.BoardingPassDto);
        }
    }
}
