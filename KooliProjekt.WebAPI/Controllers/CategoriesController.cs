using System.Threading.Tasks;
using KooliProjekt.Application.Features.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class CategoriesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetCategoryQuery query)
        {
            if (query == null || query.Id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpGet("List/{page}/{pageSize}")]
        public async Task<IActionResult> List(int page, int pageSize, [FromQuery] string name)
        {
            var query = new ListCategoriesQuery
            {
                Page = page,
                PageSize = pageSize,
                Name = name
            };

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }
    }
}
