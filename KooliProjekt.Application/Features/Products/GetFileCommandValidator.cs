using FluentValidation;

namespace KooliProjekt.Application.Features.Products
{
    public class GetFileCommandValidator : AbstractValidator<GetFileCommand>
    {
        public GetFileCommandValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("ProductId must be greater than 0.");
            RuleFor(x => x.FileName).ValidFileName();
        }
    }
}
