using FluentValidation;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQueryValidator : AbstractValidator<ListOrdersQuery>
    {
        public ListOrdersQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
            RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue);
        }
    }
}
