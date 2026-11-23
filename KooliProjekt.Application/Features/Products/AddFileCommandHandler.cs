using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult<string>>
    {
        private readonly IFileClient _fileClient;

        public AddFileCommandHandler(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public async Task<OperationResult<string>> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            // Salvestame unikaalse nimega, et sama nimega failid üksteist üle ei kirjutaks
            var extension = Path.GetExtension(request.FileName).ToLowerInvariant();
            var fileName = Guid.NewGuid().ToString() + extension;

            await _fileClient.Save(fileName, FileStore.ProductPhotos(request.ProductId), request.InputStream);

            return new OperationResult<string>(fileName);
        }
    }
}
