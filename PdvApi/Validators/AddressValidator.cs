using FluentValidation;
using PdvApi.Models;

namespace PdvApi.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.Type).NotNull().NotEmpty();
            RuleFor(c => c.Coordinates).NotNull().NotEmpty();
        }
    }
}