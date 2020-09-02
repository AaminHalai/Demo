using Demo.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoAPI.ViewModels
{
    public class DataModel
    {

        [DisplayName("Product Name")]
        public int? ProductId { get; set; }

        public SelectList ProductList { get; set; }

        public int?[] ProductIds { get; set; }

        [DisplayName("Start Date")]
        public string StartDate { get; set; }

        [DisplayName("End Date")]
        public string EndDate { get; set; }
    }

    public class ExcelDataViewModel
    {

        public int orderid { get; set; }

        public int? pinTypeId { get; set; }

        public int? paymenttype { get; set; }

        public string customerName { get; set; }
        public string fullAddress { get; set; }

        public DateTime? orderdate { get; set; }

        public int? price { get; set; }

        public int? quantity { get; set; }

        public string productname { get; set; }

    }
}