using FluentAssertions;
using FluentValidation.TestHelper;
using Inventra.Application.Inventories.Commands.CreateInventory;

namespace Inventra.Tests
{
    public class CreateInventoryCommandValidatorTests
    {
        private readonly CreateInventoryCommandValidator _validator = new();

        [Fact]
        public void Title_Empty_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand { Title = "", CategoryId = 1 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Title_TooLong_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand
            {
                Title = new string('A', 201),
                CategoryId = 1
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void CategoryId_Zero_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand { Title = "Test", CategoryId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void ValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new CreateInventoryCommand
            {
                Title = "My Inventory",
                CategoryId = 1
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Title_ExactlyMaxLength_ShouldNotHaveValidationError()
        {
            var command = new CreateInventoryCommand
            {
                Title = new string('A', 200),
                CategoryId = 1
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }
    }
}