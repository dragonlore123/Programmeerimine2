using System.Threading.Tasks;
using KooliProjekt.Application.Features.OrderItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class OrderItemsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetOrderItemQuery query)
        {
            if (query == null || query.Id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpGet("List/{page}/{pageSize}")]
        public async Task<IActionResult> List(int page, int pageSize, [FromQuery] int? orderId)
        {
            var query = new ListOrderItemsQuery
            {
                Page = page,
                PageSize = pageSize,
                OrderId = orderId
            };

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveOrderItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderItemCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }
    }
}
