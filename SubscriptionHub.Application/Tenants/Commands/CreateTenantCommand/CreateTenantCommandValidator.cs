using FluentValidation;

namespace SubscriptionHub.Application.Tenants.Commands.CreateTenantCommand
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tenant name is required.")
                .MaximumLength(200).WithMessage("Tenant name must not exceed 200 characters.");

            RuleFor(x => x.Slug)
              .NotEmpty()
              .MaximumLength(100)
              .Matches(@"^[a-z0-9-]+$").WithMessage("Slug must contain only lowercase letters, numbers and hyphens.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .MaximumLength(256)
                .EmailAddress();


        }
    }
}
