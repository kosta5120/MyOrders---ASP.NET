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
using static MyOrders.FilterConfig;

namespace MyOrders.Controllers
{
    [UserAuthenticationFilter]
    [NoDirectAccess]
    public class OrdersController : Controller
    {
        MyOrdersDal dal = new MyOrdersDal();
        GetUserID userID = new GetUserID();
        List<Orders> orders = new List<Orders>();
        Orders model = new Orders();
        //TODO: Need to check why can i add new orders to user that not exsits.
        #region Show all orders in the View
        // GET: MyOrders
        
        [HttpGet]
        public ActionResult Orders(int id)
        {
            ViewBag.userId = id;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI/" + id.ToString()).Result;
            
            orders = response.Content.ReadAsAsync<List<Orders>>().Result;
            ViewBag.Orders = orders;
            return View();
        }
        #endregion

        #region Add new order
        [HttpPost]
        public ActionResult AddNewOrder(Orders orders)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("OrdersAPI", orders).Result;

            //dal.Orders.Add(orders);
            //dal.SaveChanges();
            //return View();
            return Redirect("/Orders/Orders/" + orders.UserID);
        }
        #endregion

        #region Edit order
        [HttpGet]
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI?OrderId=" + id.ToString()).Result;
          
            model = response.Content.ReadAsAsync<Orders>().Result;
           
            ViewBag.Id = model.ID;
            ViewBag.userId = model.UserID;

            return PartialView(model);
        }
        #endregion

        #region Update order
        [HttpPost]
        public ActionResult Update(Orders orders)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("OrdersAPI/" + orders.ID.ToString(), orders).Result;
            //return View();

            return Redirect("/Orders/Orders/" + orders.UserID);
        }
        #endregion


        #region Delete order
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("OrdersAPI/" + id.ToString()).Result;
            var model = response.Content.ReadAsStringAsync().Result;
            //return View();
            return Redirect("/Orders/Orders/" + userID.UserID);
        }
        #endregion

        #region Provide order
        public ActionResult Provided(Orders orders)
        {
            var url = string.Empty;
            using (dal)
            {
                if (orders.ID != 0)
                {
                    //orders = dal.Orders.SingleOrDefault(x => x.ID == orders.ID);
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
        #endregion

    }
}