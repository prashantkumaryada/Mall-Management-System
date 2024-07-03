using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    [Table("Cart")]
    public class CartModel
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public ProductItemModel Product  { get; set; }

        public int Quantity { get; set; }
    }
}
