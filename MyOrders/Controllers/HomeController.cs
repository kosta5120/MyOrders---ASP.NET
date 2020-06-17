using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyOrders.Models;
using MyOrders.Dal;
using MyOrders.ViewModel;
using System.Web.Security;

namespace MyOrders.Controllers
{
    public class HomeController : Controller
    {
        public MyOrdersDal dal = new MyOrdersDal();
        public ActionResult LoginSignup()
        {
            return View();
        }
        
        [HttpGet]
        public JsonResult EmailExistingCheck(string emailAddress)
        {
            bool result = true;
            if (emailAddress != string.Empty)
            {
                result = !dal.Users.ToList().Exists(model => model.EmailAddress.Contains(emailAddress));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SignUp(login_Signup ls)
        {
            if (ModelState.IsValid)
            {
                var check = new ChackUserToLogIn();
                string _id;
                ls.Password = new EncryptDecrypt().Encript(ls.Password);
                ls.FirstName = ls.FirstName.ToLower();
                ls.LastName = ls.LastName.ToLower();
                ls.EmailAddress = ls.EmailAddress.ToLower();
                dal.Users.Add(ls);
                dal.SaveChanges();

                var result = check.CheckUser(dal, ls.Password, ls.EmailAddress, out _id);
                
                if (result && _id != string.Empty)
                    Session["UserID"] = Guid.NewGuid();

                int id = int.Parse(_id); 
               // return Redirect("/Orders/Orders/" + id);
                return Redirect("/Orders/Orders/" + id);
            }
            else
            {
                return View("LoginSignup");
            }
        }
        [HttpPost]
        public JsonResult CheckLogIn(string email, string pass)
        {
            var check = new ChackUserToLogIn();
            var id = string.Empty;

            pass = new EncryptDecrypt().Encript(pass);
            
            var result = check.CheckUser(dal, pass, email, out id);
            var data = new { result, id };

            if (data.result && data.id != string.Empty)
                Session["UserID"] = Guid.NewGuid();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}