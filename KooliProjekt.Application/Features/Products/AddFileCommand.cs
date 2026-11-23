using System.IO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class AddFileCommand : IRequest<OperationResult<string>>
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public long Length { get; set; }
        public Stream InputStream { get; set; }
    }
}
