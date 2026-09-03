using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionHub.Application.Subscription.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator()
        {
            //RuleFor(x => x.TenantId)
            //    .NotEmpty().WithMessage("TenantId is required.");
            RuleFor(x => x.PlanId)
                .NotEmpty().WithMessage("PlanId is required.");
            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("Customer email is required.")
                .EmailAddress().WithMessage("Customer email must be a valid email address.");   // ✅
        }
    }
}
