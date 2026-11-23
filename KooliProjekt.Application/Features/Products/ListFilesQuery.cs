using System.Collections.Generic;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public record ListFilesQuery(int ProductId) : IRequest<OperationResult<IList<string>>>;
}
