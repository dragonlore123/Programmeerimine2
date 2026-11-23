using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class SaveOrderItemCommandHandler : IRequestHandler<SaveOrderItemCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public SaveOrderItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(SaveOrderItemCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var product = await _context.Products.FindAsync(new object[] { request.ProductId }, cancellationToken);
            if (product == null)
            {
                return result.AddPropertyError("ProductId", "Cannot find product with id " + request.ProductId);
            }

            var item = new OrderItem();
            if (request.Id == 0)
            {
                item.OrderId = request.OrderId;
                item.UnitPrice = product.Price;
                _context.OrderItems.Add(item);
            }
            else
            {
                item = await _context.OrderItems.FindAsync(new object[] { request.Id }, cancellationToken);
                if (item == null)
                {
                    return result.AddError("Cannot find order item with id " + request.Id);
                }

                // Kui toode vahetatakse, võetakse uue toote hetkehind
                if (item.ProductId != request.ProductId)
                {
                    item.UnitPrice = product.Price;
                }
            }

            item.ProductId = request.ProductId;
            item.Quantity = request.Quantity;

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
