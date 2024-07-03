using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    [Table("Order")]
    public class OrderModel
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public CustomerModel Customer { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<OrderItemsModel> OrderItems { get; set; }
    }
}
