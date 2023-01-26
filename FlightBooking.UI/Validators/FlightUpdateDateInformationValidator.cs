using FlightBooking.API.Models.Request;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FlightBooking.API.Validators
{
    public class FlightUpdateDateInformationValidator : AbstractValidator<FlightUpdateDateInformationRequest>
    {
        public FlightUpdateDateInformationValidator()
        {
            // Supported formats:
            // 29 Nov 2015 4:15PM
            // 29 November 2015 4:15PM
            // 2015-11-29 16:15:00Z

            RuleFor(f => f.DepartureDate)
                .Matches(new Regex(@"([0-9]{2}\s+[a-v]{3,9}\s+[0-9]{4}\s+[0-9]{1,2}:[0-9]{2}(AM|PM))|([a-v]{3,9}\s+[0-9]{2},\s*[0-9]{4})|([0-9]{1,2}/[0-9]{1,2}/[0-9]{1,2})|([0-9]{4}-[0-9]{2}-[0-9]{2}(T| )[0-9]{2}:[0-9]{2}:[0-9]{2})",
                    RegexOptions.IgnoreCase |
                    RegexOptions.CultureInvariant |
                    RegexOptions.Multiline))
                .WithMessage("Invalid DateTime Format");

            RuleFor(f => f.ArrivingDate)
                .Matches(new Regex(@"([0-9]{2}\s+[a-v]{3,9}\s+[0-9]{4}\s+[0-9]{1,2}:[0-9]{2}(AM|PM))|([a-v]{3,9}\s+[0-9]{2},\s*[0-9]{4})|([0-9]{1,2}/[0-9]{1,2}/[0-9]{1,2})|([0-9]{4}-[0-9]{2}-[0-9]{2}(T| )[0-9]{2}:[0-9]{2}:[0-9]{2})",
                    RegexOptions.IgnoreCase |
                    RegexOptions.CultureInvariant |
                    RegexOptions.Multiline))
                .WithMessage("Invalid DateTime Format");
        }
    }
}
