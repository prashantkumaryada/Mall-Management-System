using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    [Table("Customer")]
    public class CustomerModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }
    }
}
