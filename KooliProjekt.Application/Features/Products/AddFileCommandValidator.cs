using System;
using System.IO;
using System.Linq;
using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Products
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        public const long MaxFileSize = 5 * 1024 * 1024;

        public AddFileCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ProductId must be greater than 0.")
                .Custom((productId, context) =>
                {
                    if (!dbContext.Products.Any(p => p.Id == productId))
                    {
                        context.AddFailure("ProductId", "The specified product does not exist.");
                    }
                });

            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File is required.")
                .Must(name => AllowedExtensions.Contains(Path.GetExtension(name ?? "").ToLowerInvariant()))
                .WithMessage("Allowed file types are: " + string.Join(", ", AllowedExtensions));

            RuleFor(x => x.Length)
                .GreaterThan(0).WithMessage("File is empty.")
                .LessThanOrEqualTo(MaxFileSize).WithMessage("File must not be larger than 5 MB.");

            RuleFor(x => x.InputStream)
                .NotNull().WithMessage("File is required.");
        }
    }
}
