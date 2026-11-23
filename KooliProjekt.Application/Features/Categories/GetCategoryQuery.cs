using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Categories
{
    public record GetCategoryQuery(int Id) : IRequest<OperationResult<CategoryDto>>;
}
