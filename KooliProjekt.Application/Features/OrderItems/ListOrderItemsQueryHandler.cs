using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class ListOrderItemsQueryHandler : IRequestHandler<ListOrderItemsQuery, OperationResult<PagedResult<OrderItemDto>>>
    {
        private readonly ApplicationDbContext _context;

        public ListOrderItemsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<OrderItemDto>>> Handle(ListOrderItemsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<OrderItem> query = _context.OrderItems.AsNoTracking();

            if (request.OrderId.HasValue)
            {
                query = query.Where(item => item.OrderId == request.OrderId.Value);
            }

            var result = new OperationResult<PagedResult<OrderItemDto>>();
            result.Value = await query
                .OrderBy(item => item.OrderId)
                .ThenBy(item => item.Id)
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
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
