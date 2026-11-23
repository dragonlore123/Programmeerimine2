using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Categories
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, OperationResult<CategoryDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetCategoryQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CategoryDto>();
            result.Value = await _context.Categories
                .AsNoTracking()
                .Where(category => category.Id == request.Id)
                .Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    ProductCount = category.Products.Count
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
