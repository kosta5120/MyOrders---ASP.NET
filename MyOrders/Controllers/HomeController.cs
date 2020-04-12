using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyOrders.Models;
using MyOrders.Dal;

namespace MyOrders.Controllers
{
    public class HomeController : Controller
    {
        MyOrdersDal dal = new MyOrdersDal();

        public ActionResult LoginSignup()
        {
            return View();
        }

        [HttpGet]
        public JsonResult EmailExistingCheck(string emailAddress)
        {
            
            bool result = !dal.Users.ToList().Exists(model => model.EmailAddress.Equals(emailAddress, StringComparison.CurrentCultureIgnoreCase));
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Submit(login_Signup ls)
        {
            if (ModelState.IsValid)
            {
                ls.Password = new EncryptDecrypt().Encript(ls.Password);
                ls.FirstName = ls.FirstName.ToLower();
                ls.LastName = ls.LastName.ToLower();
                ls.EmailAddress = ls.EmailAddress.ToLower();
                dal.Users.Add(ls);
                dal.SaveChanges();
                return View("LoginSignup");
            }
            else
            {
                //ViewBag.Message = "User with this Email Already Exist";
                ls.Password = "";
                return View("LoginSignup");

            }


            //return View("LoginSignup");
        }

       

    }
}