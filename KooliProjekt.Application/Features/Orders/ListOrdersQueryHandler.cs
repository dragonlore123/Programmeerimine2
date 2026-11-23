using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, OperationResult<PagedResult<OrderListItemDto>>>
    {
        private readonly ApplicationDbContext _context;

        public ListOrdersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<OrderListItemDto>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Order> query = _context.Orders.AsNoTracking();

            if (request.Status.HasValue)
            {
                query = query.Where(order => order.Status == request.Status.Value);
            }

            var result = new OperationResult<PagedResult<OrderListItemDto>>();
            result.Value = await query
                .OrderByDescending(order => order.OrderDate)
                .Select(order => new OrderListItemDto
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    CustomerName = order.CustomerName,
                    Status = order.Status,
                    ItemCount = order.Items.Count,
                    Total = order.Items.Sum(item => item.Quantity * item.UnitPrice)
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
