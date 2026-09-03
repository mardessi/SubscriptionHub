using FluentValidation;

namespace SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandValidator : AbstractValidator<CreateSubscriptionPlanCommand>
    {
        public CreateSubscriptionPlanCommandValidator()
        {
            //RuleFor(x => x.TenantId)
            //    .NotEmpty().WithMessage("TenantId is required.");
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than Zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.");


            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be a 3-letter ISO currency code.");

        }
    }
}
