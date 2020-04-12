using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyOrders.Views.Orders
{
    public class MyOrdersController : Controller
    {
        // GET: MyOrders
        public ActionResult MyOrders()
        {
            return View();
        }
    }
}