using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public record GetOrderQuery(int Id) : IRequest<OperationResult<OrderDto>>;
}
