using FluentValidation;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class ListOrderItemsQueryValidator : AbstractValidator<ListOrderItemsQuery>
    {
        public ListOrderItemsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
            RuleFor(x => x.OrderId).GreaterThan(0).When(x => x.OrderId.HasValue);
        }
    }
}
