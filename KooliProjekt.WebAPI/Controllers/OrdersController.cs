using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class OrdersController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetOrderQuery query)
        {
            if (query == null || query.Id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpGet("List/{page}/{pageSize}")]
        public async Task<IActionResult> List(int page, int pageSize, [FromQuery] OrderStatus? status)
        {
            var query = new ListOrdersQuery
            {
                Page = page,
                PageSize = pageSize,
                Status = status
            };

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }
    }
}
