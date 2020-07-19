using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class OrdersAPIController : ApiController
    {
        private MyOrdersEntities db = new MyOrdersEntities();
        
        // GET: api/OrdersAPI
        public IQueryable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET: api/OrdersAPI/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            var order = db.Orders.Where(x => x.UserID == id).ToList();

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        //GET: api/OrdersAPI?OrderId=
        [ResponseType(typeof(void))]
        public IHttpActionResult GetUserID(int OrderId)
        {
            var order = db.Orders.Find(OrderId);

            return Ok(order);
        }

        //GET: api/OrdersAPI?UserId=
        [ResponseType(typeof(void))]
        public IHttpActionResult GetProvidedOrders(int UserId)
        {
            var providedOrders = db.Orders.Where(x => x.UserID == UserId && x.Provided == 1).ToList();
            return Ok(providedOrders);
        }

        // PUT: api/OrdersAPI/5
        [ResponseType(typeof(void))]

        public IHttpActionResult PutOrder(int id, [FromBody]Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.ID)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/OrdersAPI
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();

            return Ok(order);
        }

        // DELETE: api/OrdersAPI/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);

        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.ID == id) > 0;
        }
    }
}