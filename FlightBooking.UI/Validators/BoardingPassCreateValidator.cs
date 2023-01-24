using FlightBooking.API.Models.Request;
using System.Text.RegularExpressions;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class BoardingPassCreateValidator : AbstractValidator<BoardingPassCreateRequest>
    {
        public BoardingPassCreateValidator()
        {
            RuleFor(bp => bp.FlightId).NotEmpty().WithMessage("Flight can not be empty");

            RuleFor(bp => bp.UserId).NotEmpty().WithMessage("User can not be empty");

            RuleFor(bp => bp.Prise).NotNull().NotEmpty().WithMessage("Price can not be null or empty");

            RuleFor(bp => bp.BookingExpireDate).Matches(new Regex(@"([0-9]{2}\s+[a-v]{3,9}\s+[0-9]{4}\s+[0-9]{1,2}:[0-9]{2}(AM|PM))|([a-v]{3,9}\s+[0-9]{2},\s*[0-9]{4})|([0-9]{1,2}/[0-9]{1,2}/[0-9]{1,2})|([0-9]{4}-[0-9]{2}-[0-9]{2}(T| )[0-9]{2}:[0-9]{2}:[0-9]{2})",
                    RegexOptions.IgnoreCase |
                    RegexOptions.CultureInvariant |
                    RegexOptions.Multiline))
                .WithMessage("Invalid DateTime Format");
        }
    }
}
