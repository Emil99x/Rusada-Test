using FluentValidation;
using Rusada.Core.Dto;
using System.Text.RegularExpressions;

namespace Rusada.Core.Validators
{
    public class AircraftValidator : AbstractValidator<AircraftDto>
    {
        public AircraftValidator()
        {
            RuleFor(s => s.Make).NotEmpty().MaximumLength(128);
            RuleFor(s => s.Model).NotEmpty().MaximumLength(128);
            RuleFor(s => s.Registration).NotEmpty().Must(BeValidRegisteration).WithMessage("Invalid registeration format.Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen");
            RuleFor(s => s.Location).NotEmpty().MaximumLength(225);
            RuleFor(x => x.DateTime)
           .Must(BeValidDateTimeInPast)
           .WithMessage("Date must be a valid datetime in the past");
        }

        private bool BeValidRegisteration(string registration)
        {
            if (string.IsNullOrEmpty(registration))
            {
                return false;
            }

            var regexPattern = @"^[a-zA-Z]{1,2}-[a-zA-Z]{1,5}$";
            Regex regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(registration);
        }

        private bool BeValidDateTimeInPast(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
