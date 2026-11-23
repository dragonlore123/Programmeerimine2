using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Orders
{
    public class SaveOrderCommandValidator : AbstractValidator<SaveOrderCommand>
    {
        public SaveOrderCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0).WithMessage("Id must be greater than or equal to 0.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("CustomerName is required.")
                .MaximumLength(100).WithMessage("CustomerName must not exceed 100 characters.");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("CustomerEmail is required.")
                .MaximumLength(100).WithMessage("CustomerEmail must not exceed 100 characters.")
                .EmailAddress().WithMessage("CustomerEmail is not a valid e-mail address.");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty().WithMessage("ShippingAddress is required.")
                .MaximumLength(300).WithMessage("ShippingAddress must not exceed 300 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status is not valid.")
                .Custom((status, context) =>
                {
                    var command = context.InstanceToValidate;

                    // Uus tellimus on alati staatuses New
                    if (command.Id == 0)
                    {
                        if (status != OrderStatus.New)
                        {
                            context.AddFailure("Status", "A new order must have status New.");
                        }
                        return;
                    }

                    var order = dbContext.Orders
                        .Where(o => o.Id == command.Id)
                        .Select(o => new { o.Status, ItemCount = o.Items.Count })
                        .FirstOrDefault();

                    if (order == null)
                    {
                        context.AddFailure("Id", "The specified order does not exist.");
                        return;
                    }

                    // Staatus liigub ainult edasi ja ühe sammu kaupa: New -> Paid -> Shipped
                    if (status < order.Status)
                    {
                        context.AddFailure("Status", "Order status cannot go back from " + order.Status + " to " + status + ".");
                    }
                    else if (status > order.Status + 1)
                    {
                        context.AddFailure("Status", "Order status can only change one step at a time.");
                    }
                    else if (status == OrderStatus.Paid && order.Status == OrderStatus.New && order.ItemCount == 0)
                    {
                        context.AddFailure("Status", "An order without products cannot be paid.");
                    }
                });
        }
    }
}
