using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GroceryStore.Controllers
{
    public class CustomerController : Controller
    {
        Square_Grocery_StoreEntities3 db = new Square_Grocery_StoreEntities3();
        
        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveCustomerOrder(CustomerOrder customerOrder)
        {
            object data = null;

            try
            {
                customerOrder.CustomerName = Request.Form["Name"];
                customerOrder.CustomerPhone = Request.Form["Phone"];
                customerOrder.CustomerEmail = Request.Form["Email"];
                customerOrder.OrderMainTotalAmount = Convert.ToDecimal(Request.Form["TotalPrice"]);
                customerOrder.CustomerAddress = Request.Form["Address"];
                customerOrder.OrderMainStatus = 1;
                //customerOrder.Products = new JavaScriptSerializer().Deserialize<IList<Product>>(Request.Form["Products"]);
                customerOrder.Products = new JavaScriptSerializer().Deserialize<IList<ProductMetaData>>(Request.Form["Products"]);
                customerOrder.TrackingNumber = this.GenerateTrackingNumber();

                string xmlProducts = "";
                DataSet dsxml = new DataSet();
                if (customerOrder.Products != null && customerOrder.Products.Count > 0)
                {
                    DataTable product = DTConversionHelper.ToDataTable(customerOrder.Products);
                    product.TableName = "Product";
                    dsxml.Tables.Add(product);
                    xmlProducts = dsxml.GetXml();
                }

                //customerOrder.OrderMainId = db.SaveCustomerOrder(customerOrder.CustomerName, customerOrder.CustomerPhone, customerOrder.CustomerEmail, customerOrder.OrderMainDeliveryDate, customerOrder.OrderDate, customerOrder.OrderMainTotalAmount, customerOrder.OrderMainStatus, customerOrder.TrackingNumber, customerOrder.Products);
                customerOrder.OrderMainId = db.SaveCustomerOrder(customerOrder.CustomerName, customerOrder.CustomerPhone, customerOrder.CustomerEmail, customerOrder.OrderMainTotalAmount, customerOrder.CustomerAddress, customerOrder.OrderMainStatus, customerOrder.TrackingNumber, xmlProducts);

                //data = new {Message = "SUCCESS", Result = customerOrder.TrackingNumber, Records = customerOrder.TrackingNumber };
                data = new { Result = "SUCCESS", Records = customerOrder };
            }
            catch (Exception)
            {
                throw;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private string GenerateTrackingNumber()
        {
            string trackingNumber = Guid.NewGuid().ToString();
            return trackingNumber.Substring(0, 8);
        }

        public JsonResult GetOrderDetailByCustomer()
        {
            object data = null;
            OrderDetailMetaData orderDetail = new OrderDetailMetaData();

            try
            {
                orderDetail.CustomerInfoId = Convert.ToInt64(Request.Form["CustomerInfoId"]);

                var orderDetailList = db.GetOrderDetailByCustomer(orderDetail.CustomerInfoId).ToList();
                data = new { Result = "SUCCESS", Records = orderDetailList };
            }
            catch (Exception)
            {
                throw;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}