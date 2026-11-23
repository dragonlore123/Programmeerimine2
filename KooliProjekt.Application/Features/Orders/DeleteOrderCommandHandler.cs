using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Orders
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, OperationResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await _context.OrderItems
                .Where(item => item.OrderId == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context.Orders
                .Where(order => order.Id == request.Id)
                .ExecuteDeleteAsync(cancellationToken);

            return new OperationResult();
        }
    }
}
