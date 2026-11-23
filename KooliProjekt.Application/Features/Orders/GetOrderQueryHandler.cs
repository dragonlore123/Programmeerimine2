using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OperationResult<OrderDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrderQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OrderDto>();
            result.Value = await _context.Orders
                .AsNoTracking()
                .Where(order => order.Id == request.Id)
                .Select(order => new OrderDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    CustomerName = order.CustomerName,
                    CustomerEmail = order.CustomerEmail,
                    ShippingAddress = order.ShippingAddress,
                    Status = order.Status,
                    Total = order.Items.Sum(item => item.Quantity * item.UnitPrice)
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (result.Value == null)
            {
                return result;
            }

            result.Value.Items = await _context.OrderItems
                .AsNoTracking()
                .Where(item => item.OrderId == request.Id)
                .OrderBy(item => item.Id)
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
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
