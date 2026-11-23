using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class SaveOrderItemCommandValidator : AbstractValidator<SaveOrderItemCommand>
    {
        public SaveOrderItemCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 1000).WithMessage("Quantity must be between 1 and 1000.");

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.")
                .Custom((productId, context) =>
                {
                    if (!dbContext.Products.Any(p => p.Id == productId))
                    {
                        context.AddFailure("ProductId", "The specified product does not exist.");
                    }
                });

            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("OrderId must be greater than 0.")
                .Custom((orderId, context) =>
                {
                    var command = context.InstanceToValidate;

                    // Olemasoleva rea tellimust ei saa vahetada
                    if (command.Id > 0)
                    {
                        var currentOrderId = dbContext.OrderItems
                            .Where(i => i.Id == command.Id)
                            .Select(i => (int?)i.OrderId)
                            .FirstOrDefault();

                        if (currentOrderId == null)
                        {
                            context.AddFailure("Id", "The specified order item does not exist.");
                            return;
                        }

                        if (currentOrderId != orderId)
                        {
                            context.AddFailure("OrderId", "An order item cannot be moved to another order.");
                            return;
                        }
                    }

                    var order = dbContext.Orders
                        .Where(o => o.Id == orderId)
                        .Select(o => new { o.Status })
                        .FirstOrDefault();

                    if (order == null)
                    {
                        context.AddFailure("OrderId", "The specified order does not exist.");
                        return;
                    }

                    // Makstud või tarnitud tellimust muuta ei saa
                    if (order.Status != OrderStatus.New)
                    {
                        context.AddFailure("OrderId", "Only orders with status New can be changed.");
                    }

                    // Sama toode on tellimuses ainult ühel real
                    var duplicate = dbContext.OrderItems
                        .Any(i => i.OrderId == orderId &&
                                  i.ProductId == command.ProductId &&
                                  i.Id != command.Id);

                    if (duplicate)
                    {
                        context.AddFailure("ProductId", "This product is already in the order. Change the quantity instead.");
                    }
                });
        }
    }
}
