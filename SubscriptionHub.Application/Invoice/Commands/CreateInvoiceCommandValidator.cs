using FluentValidation;

namespace SubscriptionHub.Application.Invoice.Commands
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x=>x.TenantId)
                .Empty().WithMessage("TenantId is required.");
            RuleFor(x => x.SubscriptionId)
                .Empty().WithMessage("SubscriptionId is required.");
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("Amount must be greater than Zero.");
            RuleFor(x=>x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-letter ISO currency code.");
        }
    }
}
