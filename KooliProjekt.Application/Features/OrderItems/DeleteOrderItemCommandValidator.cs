using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.")
                .Custom((id, context) =>
                {
                    var item = dbContext.OrderItems
                        .Where(i => i.Id == id)
                        .Select(i => new { i.Order.Status, ItemCount = i.Order.Items.Count })
                        .FirstOrDefault();

                    if (item == null)
                    {
                        return;
                    }

                    if (item.Status != OrderStatus.New)
                    {
                        context.AddFailure("Id", "Only orders with status New can be changed.");
                    }
                    else if (item.ItemCount == 1)
                    {
                        // Tellimus koosneb alati toodetest: viimast rida ei kustutata
                        context.AddFailure("Id", "An order must have at least one item. Delete the order instead.");
                    }
                });
        }
    }
}
