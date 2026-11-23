using FluentValidation;

namespace KooliProjekt.Application.Features.Categories
{
    public class ListCategoriesQueryValidator : AbstractValidator<ListCategoriesQuery>
    {
        public ListCategoriesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
            RuleFor(x => x.Name).MaximumLength(100);
        }
    }
}
