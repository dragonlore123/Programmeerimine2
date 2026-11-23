using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public record GetOrderItemQuery(int Id) : IRequest<OperationResult<OrderItemDto>>;
}
