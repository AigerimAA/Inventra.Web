using FluentValidation;

namespace Inventra.Application.Items.Commands.UpdateItem
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid item ID");

            RuleFor(x => x.InventoryId)
                .GreaterThan(0).WithMessage("Invalid inventory ID");

            RuleFor(x => x.Version)
                .NotEmpty().WithMessage("Version is required for concurrency check");
        }
    }
}
