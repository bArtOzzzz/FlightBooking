using FlightBooking.API.Models.Request;
using System.Text.RegularExpressions;
using FluentValidation;

namespace FlightBooking.API.Validators
{
    public class PersonInformationModelValidator : AbstractValidator<PersonInformationCreateOrUpdateRequest>
    {
        public PersonInformationModelValidator()
        {
            RuleFor(pi => pi.Citizenship).Length(3, 33).NotEmpty().WithMessage("Length should be 3 to 33 characters");

            RuleFor(pi => pi.IdentificationNumber).Length(6, 26).WithMessage("Length should be 6 to 26 characters");

            // Supported formats:
            // 29 Nov 2015 4:15PM
            // 29 November 2015 4:15PM
            // 2015-11-29 16:15:00Z

            RuleFor(pi => pi.ExpirePasportDate).Matches(new Regex(@"([0-9]{2}\s+[a-v]{3,9}\s+[0-9]{4}\s+[0-9]{1,2}:[0-9]{2}(AM|PM))|([a-v]{3,9}\s+[0-9]{2},\s*[0-9]{4})|([0-9]{1,2}/[0-9]{1,2}/[0-9]{1,2})|([0-9]{4}-[0-9]{2}-[0-9]{2}(T| )[0-9]{2}:[0-9]{2}:[0-9]{2})",
                    RegexOptions.IgnoreCase |
                    RegexOptions.CultureInvariant |
                    RegexOptions.Multiline))
                .WithMessage("Invalid DateTime Format");

            RuleFor(pi => pi.Name).Length(3, 33).NotEmpty().WithMessage("Length should be 3 to 33 characters");

            RuleFor(pi => pi.Surname).Length(3, 33).NotEmpty().WithMessage("Length should be 3 to 33 characters");

            RuleFor(pi => pi.BirthDate).Matches(new Regex(@"([0-9]{2}\s+[a-v]{3,9}\s+[0-9]{4}\s+[0-9]{1,2}:[0-9]{2}(AM|PM))|([a-v]{3,9}\s+[0-9]{2},\s*[0-9]{4})|([0-9]{1,2}/[0-9]{1,2}/[0-9]{1,2})|([0-9]{4}-[0-9]{2}-[0-9]{2}(T| )[0-9]{2}:[0-9]{2}:[0-9]{2})",
                    RegexOptions.IgnoreCase |
                    RegexOptions.CultureInvariant |
                    RegexOptions.Multiline))
                .WithMessage("Invalid DateTime Format");

            RuleFor(pi => pi.Gender).Length(4, 14).NotEmpty().WithMessage("Length should be 4 to 14 characters");
        }
    }
}
