using System.IO;
using FluentValidation;

namespace KooliProjekt.Application.Features.Products
{
    public static class FileNameRules
    {
        // Failinimi peab olema ainult nimi, mitte tee (keelab nt ../)
        public static IRuleBuilderOptions<T, string> ValidFileName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(100).WithMessage("FileName must not exceed 100 characters.")
                .Must(name => name == Path.GetFileName(name) && !name.Contains(".."))
                .WithMessage("FileName must not contain a path.");
        }
    }
}
