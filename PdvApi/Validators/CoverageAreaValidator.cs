using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using PdvApi.Models;

namespace PdvApi.Validators
{
    [ExcludeFromCodeCoverage]
    public class CoverageAreaValidator : AbstractValidator<CoverageArea>
    {
        public CoverageAreaValidator()
        {
            RuleFor(c => c.Type).NotNull().NotEmpty();
            RuleFor(c => c.Coordinates).NotNull().NotEmpty();
        }
    }
}