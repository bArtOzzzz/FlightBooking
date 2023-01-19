using FlightBooking.API.Models.Request;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class AirlineModelValidator : AbstractValidator<AirlineCreateOrUpdate>
    {
        public AirlineModelValidator()
        {
            RuleFor(a => a.AirlineName).Length(3, 22)
                                       .WithMessage("Length should be 3 to 22 characters");

            RuleFor(a => a.Rating).InclusiveBetween(0.0, 5.0)
                                  .WithMessage("Rating can be only between 0.0 and 5.0");
        }
    }
}
