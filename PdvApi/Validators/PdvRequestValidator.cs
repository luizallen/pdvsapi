using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using PdvApi.Models;

namespace PdvApi.Validators
{
    [ExcludeFromCodeCoverage]
    public class PdvRequestValidator : AbstractValidator<Pdv>
    {
        public PdvRequestValidator()
        {
            RuleFor(c => c.TradingName).NotNull().NotEmpty();
            RuleFor(c => c.OwnerName).NotNull().NotEmpty();
            RuleFor(c => c.Document).NotNull().NotEmpty();
            RuleFor(c => c.Document).Length(18);
            RuleFor(c => c.CoverageArea).NotNull();
            RuleFor(c => c.Address).NotNull();
        }
    }
}
