using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataAccess.Models
{
    public class Order
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public string paymenttype { get; set; }
        public string salepintype { get; set; }

        public DateTime? orderdate { get; set; }

        public int?  price { get; set; }
        public int? quantity { get; set; }
        public int? customerid { get; set; }

        public int? productid { get; set; }
    }
}
