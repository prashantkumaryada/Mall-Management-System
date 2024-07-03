using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    [Table("OrderItem")]
    public class OrderItemsModel
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public OrderModel Order { get; set; }

        [ForeignKey("ProductItem")]
        public int ProductItemId { get; set; }

        public ProductItemModel ProductItem { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
