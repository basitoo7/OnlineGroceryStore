using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class CustomerOrder
    {
        //public IList<Product> Products { get; set; }
        public IList<ProductMetaData> Products { get; set; }
        public long CustomerInfoId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string TrackingNumber { get; set; }
        public decimal? OrderMainTotalAmount { get; set; }
        public byte? OrderMainStatus { get; set; }
        public ObjectResult<long?> OrderMainId { get; set; }
    }
}