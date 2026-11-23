using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(300)]
        public string ShippingAddress { get; set; }

        public OrderStatus Status { get; set; }

        public IList<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
            Status = OrderStatus.New;
        }

        public decimal GetTotal()
        {
            return Items.Sum(item => item.GetLineTotal());
        }
    }
}
