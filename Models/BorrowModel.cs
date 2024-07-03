using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MallRAj.Models
{
    public class BorrowModel
    {

        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhoneNumber { get; set; }


        public int TotalAmount { get; set; }

        public int Paid { get; set; }

        public int Dues { get; set; }
    }
}
