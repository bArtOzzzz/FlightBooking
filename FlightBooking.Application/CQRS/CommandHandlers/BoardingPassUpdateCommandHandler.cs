using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Commands;
using MediatR;

namespace FlightBooking.Application.CQRS.CommandHandlers
{
    public class BoardingPassUpdateCommandHandler : IRequestHandler<BoardingPassUpdateCommand, Guid>
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassUpdateCommandHandler(IBoardingPassService boardingPassService) => _boardingPassService = boardingPassService;

        public async Task<Guid> Handle(BoardingPassUpdateCommand request, CancellationToken cancellationToken)
        {
            return await _boardingPassService.UpdateAsync(request.id, request.BoardingPassDto);
        }
    }
}
