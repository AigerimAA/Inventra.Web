using FluentValidation;

namespace Inventra.Application.Inventories.Commands.CreateInventory
{
    public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
    {
        public CreateInventoryCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Category is required");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Owner is required");

        }
    }
}
