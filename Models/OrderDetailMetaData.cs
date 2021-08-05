using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class OrderDetailMetaData
    {
        public long CustomerInfoId { get; set; }



        public string Title { get; set; }
        public double Qty { get; set; }
        public string UnitText { get; set; }
        public decimal UnitPrice { get; set; }
        public long OrderMainId { get; set; }
        //public BKDTO.Enumerations.OrderProgressStatus OrderStatus { get; set; }
        public string OrderStatus { get; set; }
        public string TrackingNumber { get; set; }
    }
}