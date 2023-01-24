using FlightBooking.Application.Abstractions.IServices;
using FlightBooking.Application.CQRS.Queries;
using FlightBooking.Application.Dto;
using MediatR;

namespace FlightBooking.Application.CQRS.QueryHandlers
{
    public class BoardingPassGetAllQueryHandler : IRequestHandler<BoardingPassGetAllQuery, List<BoardingPassDto>>
    {
        private readonly IBoardingPassService _boardingPassService;

        public BoardingPassGetAllQueryHandler(IBoardingPassService boardingPassService) => _boardingPassService = boardingPassService;

        public async Task<List<BoardingPassDto>> Handle(BoardingPassGetAllQuery request, CancellationToken cancellationToken)
        {
            return await _boardingPassService.GetAllAsync();
        }
    }
}
