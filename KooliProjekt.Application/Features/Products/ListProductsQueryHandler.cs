using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Products
{
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, OperationResult<PagedResult<ProductListItemDto>>>
    {
        private readonly ApplicationDbContext _context;

        public ListProductsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<ProductListItemDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _context.Products.AsNoTracking();

            if (request.CategoryId.HasValue)
            {
                query = query.Where(product => product.CategoryId == request.CategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(product => product.Name.Contains(request.Name));
            }

            var result = new OperationResult<PagedResult<ProductListItemDto>>();
            result.Value = await query
                .OrderBy(product => product.Name)
                .Select(product => new ProductListItemDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    CategoryName = product.Category.Name
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
