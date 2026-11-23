using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Categories
{
    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.")
                .Custom((id, context) =>
                {
                    if (dbContext.Products.Any(p => p.CategoryId == id))
                    {
                        context.AddFailure("Id", "Cannot delete a category that has products.");
                    }
                });
        }
    }
}
