using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using PdvApi.Models;

namespace PdvApi.Validators
{
    [ExcludeFromCodeCoverage]
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.Type).NotNull().NotEmpty();
            RuleFor(c => c.Coordinates).NotNull().NotEmpty();
        }
    }
}