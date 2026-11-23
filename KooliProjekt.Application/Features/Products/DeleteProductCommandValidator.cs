using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Products
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.")
                .Custom((id, context) =>
                {
                    if (dbContext.OrderItems.Any(i => i.ProductId == id))
                    {
                        context.AddFailure("Id", "Cannot delete a product that is used in orders.");
                    }
                });
        }
    }
}
