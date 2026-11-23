using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public SaveProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var product = new Product();
            if (request.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
                if (product == null)
                {
                    return result.AddError("Cannot find product with id " + request.Id);
                }
            }

            // Hinna muutmine ei mõjuta vanu tellimusi, sest tellimuse real on oma UnitPrice
            product.Name = request.Name.Trim();
            product.Description = request.Description?.Trim();
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
