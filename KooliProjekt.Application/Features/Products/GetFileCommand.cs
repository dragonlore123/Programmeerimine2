using System.IO;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class GetFileCommand : IRequest<OperationResult<Stream>>
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
    }
}
