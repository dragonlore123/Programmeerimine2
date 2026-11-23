using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class ListFilesQueryHandler : IRequestHandler<ListFilesQuery, OperationResult<IList<string>>>
    {
        private readonly IFileClient _fileClient;

        public ListFilesQueryHandler(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public async Task<OperationResult<IList<string>>> Handle(ListFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _fileClient.List(FileStore.ProductPhotos(request.ProductId));

            return new OperationResult<IList<string>>(files);
        }
    }
}
