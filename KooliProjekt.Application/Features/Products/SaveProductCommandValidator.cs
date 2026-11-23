using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Products
{
    public class SaveProductCommandValidator : AbstractValidator<SaveProductCommand>
    {
        public SaveProductCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .LessThanOrEqualTo(100000).WithMessage("Price must not exceed 100 000.")
                .PrecisionScale(18, 2, true).WithMessage("Price can have at most 2 decimal places.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.")
                .Custom((categoryId, context) =>
                {
                    if (!dbContext.Categories.Any(c => c.Id == categoryId))
                    {
                        context.AddFailure("CategoryId", "The specified category does not exist.");
                    }
                });
        }
    }
}
