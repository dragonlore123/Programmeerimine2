using System.IO;
using System.Threading.Tasks;
using KooliProjekt.Application.Features.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] GetProductQuery query)
        {
            if (query == null || query.Id <= 0)
            {
                return NotFound();
            }

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpGet("List/{page}/{pageSize}")]
        public async Task<IActionResult> List(int page, int pageSize, [FromQuery] int? categoryId, [FromQuery] string name)
        {
            var query = new ListProductsQuery
            {
                Page = page,
                PageSize = pageSize,
                CategoryId = categoryId,
                Name = name
            };

            var result = await _mediator.Send(query);
            return Result(result);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] SaveProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpPost("{productId}/Files")]
        public async Task<IActionResult> AddFile(int productId, IFormFile file)
        {
            if (file == null)
            {
                return BadRequest("File is required.");
            }

            using var inputStream = file.OpenReadStream();

            var command = new AddFileCommand
            {
                ProductId = productId,
                FileName = file.FileName,
                Length = file.Length,
                InputStream = inputStream
            };

            var result = await _mediator.Send(command);
            return Result(result);
        }

        [HttpGet("{productId}/Files")]
        public async Task<IActionResult> ListFiles(int productId)
        {
            var result = await _mediator.Send(new ListFilesQuery(productId));
            return Result(result);
        }

        [HttpGet("{productId}/Files/{fileName}")]
        public async Task<IActionResult> GetFile(int productId, string fileName)
        {
            var command = new GetFileCommand
            {
                ProductId = productId,
                FileName = fileName
            };

            var result = await _mediator.Send(command);
            if (result.HasErrors)
            {
                return Result(result);
            }

            if (result.Value == null)
            {
                return NotFound();
            }

            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(result.Value, contentType);
        }

        [HttpDelete("{productId}/Files/{fileName}")]
        public async Task<IActionResult> DeleteFile(int productId, string fileName)
        {
            var command = new DeleteFileCommand
            {
                ProductId = productId,
                FileName = fileName
            };

            var result = await _mediator.Send(command);
            return Result(result);
        }
    }
}
