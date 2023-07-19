using apsys.heartbeat.authorization;
using FluentValidation;

namespace apsys.heartbeat.validators
{
    public class ApplicationRoleValidator : AbstractValidator<ApplicationRole>
    {
        public ApplicationRoleValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("The [Name] cannot be null or empty");
        }
    }
}
