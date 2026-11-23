using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class GetFileCommandHandler : IRequestHandler<GetFileCommand, OperationResult<Stream>>
    {
        private readonly IFileClient _fileClient;

        public GetFileCommandHandler(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public async Task<OperationResult<Stream>> Handle(GetFileCommand request, CancellationToken cancellationToken)
        {
            var stream = await _fileClient.Get(request.FileName, FileStore.ProductPhotos(request.ProductId));

            return new OperationResult<Stream>(stream);
        }
    }
}
