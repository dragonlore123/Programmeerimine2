using System;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Dto
{
    public class OrderListItemDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus Status { get; set; }
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
    }
}
