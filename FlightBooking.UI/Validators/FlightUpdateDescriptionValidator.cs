using FlightBooking.API.Models.Request;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class FlightUpdateDescriptionValidator : AbstractValidator<FlightUpdateDescriptionRequest>
    {
        public FlightUpdateDescriptionValidator()
        {
            RuleFor(f => f.Arrival).Length(2, 33).WithMessage("Length should be 3 to 33 characters");

            RuleFor(f => f.Departurer).Length(2, 33).WithMessage("Length should be 3 to 33 characters");
        }
    }
}
