using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Orders
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.")
                .Custom((id, context) =>
                {
                    // Makstud ja tarnitud tellimusi ei kustutata
                    var isPaidOrShipped = dbContext.Orders
                        .Any(o => o.Id == id && o.Status != OrderStatus.New);

                    if (isPaidOrShipped)
                    {
                        context.AddFailure("Id", "Only orders with status New can be deleted.");
                    }
                });
        }
    }
}
