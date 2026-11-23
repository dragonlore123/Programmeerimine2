using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Application.Data
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }
        public int OrderId { get; set; }

        [Required]
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public decimal GetLineTotal()
        {
            return Quantity * UnitPrice;
        }
    }
}
