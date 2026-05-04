using FluentValidation;

namespace Inventra.Application.Access.Commands.AddAccess
{
    public class AddAccessCommandValidator : AbstractValidator<AddAccessCommand>
    {
        public AddAccessCommandValidator()
        {
            RuleFor(x => x.InventoryId).GreaterThan(0).WithMessage("Invalid inventory ID");
            RuleFor(x => x.TargetUserId).NotEmpty().WithMessage("User ID is required");
        }
    }
}
