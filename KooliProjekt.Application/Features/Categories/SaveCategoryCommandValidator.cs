using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Categories
{
    public class SaveCategoryCommandValidator : AbstractValidator<SaveCategoryCommand>
    {
        public SaveCategoryCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .Custom((name, context) =>
                {
                    var command = context.InstanceToValidate;
                    var exists = dbContext.Categories
                        .Any(c => c.Name == command.Name && c.Id != command.Id);

                    if (exists)
                    {
                        context.AddFailure("Name", "A category with the same name already exists.");
                    }
                });
        }
    }
}
