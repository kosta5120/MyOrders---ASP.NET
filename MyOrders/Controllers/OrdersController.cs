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
using System.Net.Http;

namespace MyOrders.Controllers
{

    public class OrdersController : Controller
    {
        MyOrdersDal dal = new MyOrdersDal();
        GetUserID userID = new GetUserID();
        // GET: MyOrders

        [UserAuthenticationFilter]
        [HttpGet]
        public ActionResult Orders(int id)
        {
            ViewBag.userId = id;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI/" + id.ToString()).Result;
            List<Orders> orders = new List<Orders>();
            orders = response.Content.ReadAsAsync<List<Orders>>().Result;
            ViewBag.Orders = orders;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewOrder(Orders orders)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("OrdersAPI", orders).Result;

                //dal.Orders.Add(orders);
                //dal.SaveChanges();

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
            //GetUserID userID = new GetUserID();

            userID.UserID = orders.ID;
                
            dal.Entry(orders).State = EntityState.Deleted;
            dal.SaveChanges();
            return Redirect("/Orders/Orders/" + userID.UserID);
        }
        
        public ActionResult Provided(Orders orders)
        {
            var url = string.Empty;
            using (dal)
            {
                if(orders.ID != 0)
                {
                    orders = dal.Orders.SingleOrDefault(x => x.ID == orders.ID);
                    if (orders != null)
                    {
                        orders.Provided = 1;
                        dal.Entry(orders).State = EntityState.Modified;
                        dal.SaveChanges();
                        url = "/Orders/Orders/" + userID.UserID;
                    }
                }
                else
                {
                    
                    url = "/Orders/Orders/" + userID.UserID;
                }
                
            }
            return Redirect(url);
        }

        
    }
}