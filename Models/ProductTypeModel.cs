using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj
{
    [Table("ProductType")]
    public class ProductTypeModel
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
