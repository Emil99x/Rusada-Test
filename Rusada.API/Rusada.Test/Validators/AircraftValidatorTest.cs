using FluentValidation.TestHelper;
using Rusada.Core.Dto;
using Rusada.Core.Validators;

namespace Rusada.Test.Validators
{
    public class AircraftValidatorTest
    {
        private readonly AircraftValidator _validator;

        public AircraftValidatorTest()
        {
            _validator = new AircraftValidator();
        }

        /// <summary>
        /// Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen
        /// </summary>
        [Fact]
        public void should_validate_registeration_fail()
        {
            var model = new AircraftDto()
            {
                DateTime = DateTime.Now.AddDays(-1),
                Make = "Boeing",
                Model = "777 - 300ER",
                Registration = "GYU-RNAC",
                Location = "London Gatwick"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Registration);
        }


        /// <summary>
        /// Format: 1-5 characters for suffix after a 1-2 character prefix, separated by a hyphen
        /// </summary>
        [Fact]
        public void should_validate_registeration_success()
        {
            var model = new AircraftDto()
            {
                DateTime = DateTime.Now.AddDays(-1),
                Make = "Boeing",
                Model = "777 - 300ER",
                Registration = "GY-RNAC",
                Location = "London Gatwick"
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(c => c.Registration);
        }

        [Fact]
        public void should_validate_make_fail()
        {
            var model = new AircraftDto()
            {
                DateTime = DateTime.Now.AddDays(-1),
                Make = string.Empty,
                Model = "777 - 300ER",
                Registration = "GY-RNAC",
                Location = "London Gatwick"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Make).WithErrorMessage("'Make' must not be empty.");
        }

        [Fact]
        public void should_validate_make_length_fail()
        {
            var model = new AircraftDto()
            {
                DateTime = DateTime.Now.AddDays(-1),
                Make = "blabla bla bla bla bla bla blabla bla bla bla bla blablabla bla bla bla bla blablabla bla bla bla bla blablabla bla bla bla bla bla",
                Model = "777- 300ER",
                Registration = "GY-RNAC",
                Location = "London Gatwick"
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(c => c.Make);
        }
    }
}
