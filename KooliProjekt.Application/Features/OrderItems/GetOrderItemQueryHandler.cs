using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, OperationResult<OrderItemDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrderItemQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<OrderItemDto>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OrderItemDto>();
            result.Value = await _context.OrderItems
                .AsNoTracking()
                .Where(item => item.Id == request.Id)
                .Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.Quantity * item.UnitPrice
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
