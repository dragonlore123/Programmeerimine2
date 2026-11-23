using System;
using System.Collections.Generic;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }

        public IList<OrderItemDto> Items { get; set; }

        public OrderDto()
        {
            Items = new List<OrderItemDto>();
        }
    }
}
