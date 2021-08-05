using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class LastestOrderController : Controller
    {

        Square_Grocery_StoreEntities3 db = new Square_Grocery_StoreEntities3();

        // GET: LastestOrder
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLatestOrders()
        //public ActionResult GetProductsByCategory(int categoryId)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            var Orders = db.GetAllOrder().ToList();
            object data = null;
            try
            {

                if (Orders != null)
                {
                    data = new { Result = "SUCCESS", Records = Orders };
                }
                else
                {
                    data = new { Result = "ERROR", Message = "Something went wrong" };
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
            //return View(products);
            return Json(data, JsonRequestBehavior.AllowGet);

            //ViewBag.Products = products;
            //return PartialView("~/Views/Shared/_GetProductsByCategory.cshtml");
        }

    }
}