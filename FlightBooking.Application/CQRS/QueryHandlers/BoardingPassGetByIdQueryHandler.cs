using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class BoardingPassGetByIdQueryHandler : IRequestHandler<BoardingPassGetByIdQuery, BoardingPassDto>
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassGetByIdQueryHandler(IBoardingPassService boardingPassService) => _boardingPassService = boardingPassService;

        public async Task<BoardingPassDto> Handle(BoardingPassGetByIdQuery request, CancellationToken cancellationToken)
        {
            return await _boardingPassService.GetByIdAsync(request.id);
        }
    }
}
