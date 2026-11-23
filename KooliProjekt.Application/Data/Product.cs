using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Application.Data
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public IList<OrderItem> OrderItems { get; set; }

        public Product()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
