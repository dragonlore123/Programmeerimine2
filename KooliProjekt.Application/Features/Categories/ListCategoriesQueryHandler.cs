using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Categories
{
    public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, OperationResult<PagedResult<CategoryDto>>>
    {
        private readonly ApplicationDbContext _context;

        public ListCategoriesQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<PagedResult<CategoryDto>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Category> query = _context.Categories.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(category => category.Name.Contains(request.Name));
            }

            var result = new OperationResult<PagedResult<CategoryDto>>();
            result.Value = await query
                .OrderBy(category => category.Name)
                .Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    ProductCount = category.Products.Count
                })
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
