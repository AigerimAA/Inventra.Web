using FluentValidation;

namespace Inventra.Application.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {
            RuleFor(x => x.InventoryId)
                .GreaterThan(0).WithMessage("Inventory ID is required");

            RuleFor(x => x.CreatedById)
                .NotEmpty().WithMessage("Creator ID is required");
        }
    }
}
