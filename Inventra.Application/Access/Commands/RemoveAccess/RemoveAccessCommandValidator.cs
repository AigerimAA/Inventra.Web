using FluentValidation;

namespace Inventra.Application.Access.Commands.RemoveAccess
{
    public class RemoveAccessCommandValidator : AbstractValidator<RemoveAccessCommand>
    {
        public RemoveAccessCommandValidator()
        {
            RuleFor(x => x.InventoryId).GreaterThan(0).WithMessage("Invalid inventory ID");
            RuleFor(x => x.TargetUserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
