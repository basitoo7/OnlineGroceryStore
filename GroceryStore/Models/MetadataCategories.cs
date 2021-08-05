using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GroceryStore.Models
{
    public class MetadataCategories
    {
        [Display(Name = "Categories Name")]
        public string name { get; set; }
        [Display(Name = "Cat Decrp")]
        public string description { get; set; }

    }
    [MetadataType(typeof(MetadataCategories))]
    public partial class Catagory
    {
        

    }
}