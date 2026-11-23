using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Categories
{
    public class SaveCategoryCommandHandler : IRequestHandler<SaveCategoryCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public SaveCategoryCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(SaveCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var category = new Category();
            if (request.Id == 0)
            {
                _context.Categories.Add(category);
            }
            else
            {
                category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);
                if (category == null)
                {
                    return result.AddError("Cannot find category with id " + request.Id);
                }
            }

            category.Name = request.Name.Trim();

            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
