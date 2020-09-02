using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DataAccess.SPModels
{
    public partial class usp_GetOrderList_Result
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public string paymenttype { get; set; }

        public DateTime? orderdate { get; set; }

        public int? price { get; set; }
        public int? quantity { get; set; }
        public int? customerid { get; set; }

        public int? productid { get; set; }

        public string customername { get; set; }

        public string productname { get; set; }

        public int TotalCount { get; set; }

        public int FilterCount { get; set; }
    }
}
