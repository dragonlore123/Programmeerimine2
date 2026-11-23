using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public record GetProductQuery(int Id) : IRequest<OperationResult<ProductDto>>;
}
