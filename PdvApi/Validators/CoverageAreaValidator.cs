using FluentValidation;
using PdvApi.Models;

namespace PdvApi.Validators
{
    public class CoverageAreaValidator : AbstractValidator<CoverageArea>
    {
        public CoverageAreaValidator()
        {
            RuleFor(c => c.Type).NotNull().NotEmpty();
            RuleFor(c => c.Coordinates).NotNull().NotEmpty();
        }
    }
}