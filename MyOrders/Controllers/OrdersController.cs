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
    
        #region Show all orders in the View
        // GET: MyOrders
        [HttpGet]
        public ActionResult Orders()
        {
            ViewBag.userId = Session["UserID"].ToString();
            
           HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI/" + Session["UserID"].ToString()).Result;

            orders = response.Content.ReadAsAsync<List<Orders>>().Result;
            //ViewBag.Orders = orders;
            orders = orders.Where(x => x.Provided != 1).ToList();
            return View(orders);
        }
        #endregion

        #region Add new order
        [HttpPost]
        public ActionResult AddNewOrder(Orders orders)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("OrdersAPI", orders).Result;

            return RedirectToAction("Orders");
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
 
        public ActionResult Update(Orders orders)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("OrdersAPI/" + orders.ID.ToString(), orders).Result;
           
            return RedirectToAction("Orders");
        }
        #endregion

        #region Delete order
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("OrdersAPI/" + id.ToString()).Result;
            var model = response.Content.ReadAsStringAsync().Result;
            
            return RedirectToAction("Orders");
        }
        #endregion

        #region Provide order
        public ActionResult Provided(int? id)
        {
            if(id != null )
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI?OrderId=" + id.ToString()).Result;
                model = response.Content.ReadAsAsync<Orders>().Result;
                model.Provided = 1;

                return RedirectToAction("Update", model);
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("OrdersAPI/" + Session["UserID"].ToString()).Result;
                orders = response.Content.ReadAsAsync<List<Orders>>().Result;
                orders = orders.Where(x => x.Provided == 1).ToList();

                return View(orders);
            }
           
        }
        #endregion

        #region Log Out
        public ActionResult LogOut()
        {
            Session["UserID"] = null;
            Session.Abandon();
            return RedirectToAction("LoginSignup", "Home");
        }
        #endregion

    }
}