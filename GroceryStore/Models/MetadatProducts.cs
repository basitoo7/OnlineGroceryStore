using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class MetadatProducts
    {
        [Display(Name = "Image Path")]
        public string image_path { get; set; }
        [Display(Name = "Product Name")]
        public string product_name { get; set; }
        [Display(Name = "Product Price(Rs)")]
        public string Product_price { get; set; }
        [Display(Name = "Products Descriptions")]
        public string product_details { get; set; }


    }
    [MetadataType(typeof(MetadatProducts))]
    public partial class Product
    {


    }
}