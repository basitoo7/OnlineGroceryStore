using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{

    //public partial class Product
    //{

    //}

    public class ProductMetaData
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreatedDate { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        //public bool IsActive { get; set; }
        //public string UnitText { get; set; }
        public double Quantity { get; set; }
        //public long UnitId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}