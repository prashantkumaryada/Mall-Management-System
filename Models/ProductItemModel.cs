using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    [Table("ProductItem")]
    public class ProductItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public double AvailableQuantity { get; set; }

        public string ImagePath { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>
        [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }

        public ProductTypeModel ProductType { get; set; }
    }
}
