using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class SaveOrderCommandHandler : IRequestHandler<SaveOrderCommand, OperationResult<int>>
    {
        private readonly ApplicationDbContext _context;

        public SaveOrderCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<int>> Handle(SaveOrderCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<int>();

            var order = new Order();
            if (request.Id == 0)
            {
                order.OrderDate = DateTime.Now;
                _context.Orders.Add(order);
            }
            else
            {
                order = await _context.Orders.FindAsync(new object[] { request.Id }, cancellationToken);
                if (order == null)
                {
                    return result.AddError("Cannot find order with id " + request.Id);
                }
            }

            order.CustomerName = request.CustomerName.Trim();
            order.CustomerEmail = request.CustomerEmail.Trim();
            order.ShippingAddress = request.ShippingAddress.Trim();
            order.Status = request.Status;

            await _context.SaveChangesAsync(cancellationToken);

            // Tagastame ID, et uuele tellimusele saaks kohe ridu lisada
            result.Value = order.Id;
            return result;
        }
    }
}
