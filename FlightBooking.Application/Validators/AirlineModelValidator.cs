using FlightBooking.Application.Dto;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class AirlineModelValidator : AbstractValidator<AirlineDto>
    {
        public AirlineModelValidator()
        {
            RuleFor(a => a.AirlineName).NotNull().NotEmpty();           

            RuleFor(a => a.Rating).InclusiveBetween(0.0, 5.0)
                                  .WithMessage("Rating can be only between 0.0 and 5.0");
        }
    }
}
