using FluentValidation;

namespace VerticalSliceTemplate.Features.Taxes.Command.CreateTax
{
    public class CreateTaxValidator : AbstractValidator<CreateTaxCommand>
    {
        public CreateTaxValidator()
        {
            RuleFor(v => v.Year)
                .GreaterThan(2000)
                .LessThan(2025);
        }

    }
}
