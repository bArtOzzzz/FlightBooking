using FlightBooking.API.Models.Request;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class AirplaneModelValidator : AbstractValidator<AirplaneCreateOrUpdateRequest>
    {
        public AirplaneModelValidator()
        {
            RuleFor(a => a.ModelName).Length(3, 33).WithMessage("Length should be 3 to 22 characters");

            RuleFor(a => a.MaximumSeats).InclusiveBetween(1, 999).WithMessage("Amount of seats should be in range from 1 to 999");

            RuleFor(a => a.MaximumWeight).InclusiveBetween(0, 100000).WithMessage("Carrying capacity should be in range from 0 to 100 000");
        }
    }
}
