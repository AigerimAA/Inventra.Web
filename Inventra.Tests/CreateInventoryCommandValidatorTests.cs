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
            var command = new CreateInventoryCommand { Title = "", OwnerId = "user-1", CategoryId = 1 };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Title_TooLong_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand
            {
                Title = new string('A', 201),
                OwnerId = "user-1",
                CategoryId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void CategoryId_Zero_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand { Title = "Test", OwnerId = "user-1", CategoryId = 0 };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.CategoryId);
        }

        [Fact]
        public void OwnerId_Empty_ShouldHaveValidationError()
        {
            var command = new CreateInventoryCommand { Title = "Test", OwnerId = "", CategoryId = 1 };

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.OwnerId);
        }

        [Fact]
        public void ValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new CreateInventoryCommand
            {
                Title = "My Inventory",
                OwnerId = "user-123",
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
                OwnerId = "user-1",
                CategoryId = 1
            };

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }
    }
}
