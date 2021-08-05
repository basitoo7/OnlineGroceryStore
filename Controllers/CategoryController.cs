using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class CategoryController : Controller
    {
        db_of_my_projectEntities1 db = new db_of_my_projectEntities1();
        //
        // GET: /Category/
        public ActionResult Index()
        {
            IEnumerable<Catagory> categories = db.Catagories.ToList();
            return View(categories);
        }


        //public ActionResult ProceedToCheckOut()
        //{
        //    return View();
        //}

	}
}