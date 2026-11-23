using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Products
{
    public class ListFilesQueryValidator : AbstractValidator<ListFilesQuery>
    {
        public ListFilesQueryValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.")
                .Custom((productId, context) =>
                {
                    if (!dbContext.Products.Any(p => p.Id == productId))
                    {
                        context.AddFailure("ProductId", "The specified product does not exist.");
                    }
                });
        }
    }
}
