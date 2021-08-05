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
        Square_Grocery_StoreEntities3 db = new Square_Grocery_StoreEntities3();
        //
        // GET: /Category/
        public ActionResult Index()
        {
            IEnumerable<Catagory> categories = db.Catagories.ToList();
            return View(categories);
        }


        public JsonResult Authenticate()
        {
            object data = null;
            try
            {

                UserRequest req = new UserRequest();

                req.User = new User();

                string userName = Request["UsernName"].ToString();
                string password = Request["UserPassword"].ToString();

                req.User.Email = userName;
                req.User.Password = password;

                var response = db.UM_AuthenticateUser(userName, password);
                //UserResponse response = new UserResponse();
                //response.AppUser = (User)returnedObject;
                //if (response.AppUser != null) //if user is authenticated
                if (response != null) //if user is authenticated
                {
                    LoggedInUser user = new LoggedInUser();

                    //user.FullName = response.AppUser.FullName;
                    //user.FullName = response.FullName;
                    //user.Phone = response.AppUser.Phone;
                    //user.UserId = response.AppUser.UserId;
                    //user.RoleId = response.AppUser.CatUserRoleId;
                    //user.Role = response.AppUser.Role;
                    //user.Email = response.AppUser.Email;
                    //user.Designation = response.AppUser.Designation;

                    //Session[Appconstants.SessionConstants.USER_INFO] = user;

                    data = new { Result = "SUCCESS", Records = response };

                }

                

            }

            catch (Exception)
            {
                throw;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}