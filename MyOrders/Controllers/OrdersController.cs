using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyOrders.Models;
using MyOrders.Dal;
using MyOrders.Filters;
using System.Security.Claims;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using MyOrders.ViewModel;

namespace MyOrders.Controllers
{

    public class OrdersController : Controller
    {
        MyOrdersDal dal = new MyOrdersDal();
        // GET: MyOrders

        [UserAuthenticationFilter]
        public ActionResult Orders(int id)
        {
            ViewBag.userId = id;
            ViewBag.Orders = dal.Orders.Where(model => model.UserID == id).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddNewOrder(Orders orders)
        {
           
            dal.Orders.Add(orders);
            dal.SaveChanges();

            return Redirect("/Orders/Orders/" + orders.UserID);
        }

        public ActionResult Edit(int id)
        {
            Orders updateOrder = new Orders();

            updateOrder = dal.Orders.Find(id);
            ViewBag.Id = updateOrder.ID;
            ViewBag.userId = updateOrder.UserID;
            return PartialView(updateOrder);
        }

        public ActionResult Update(Orders orders)
        {

            dal.Entry(orders).State = EntityState.Modified;
            dal.SaveChanges();
            return Redirect("/Orders/Orders/" + orders.UserID);
        }
        public ActionResult Delete(Orders orders)
        {
            GetUserID userID = new GetUserID();

            userID.UserID = orders.ID;
                
            dal.Entry(orders).State = EntityState.Deleted;
            dal.SaveChanges();
            return Redirect("/Orders/Orders/" + userID.UserID);
        }
    }
}