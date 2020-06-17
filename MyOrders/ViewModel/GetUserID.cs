using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MyOrders.Dal;
using MyOrders.Models;


namespace MyOrders.ViewModel
{
    public class GetUserID 
    {
        private int _id;
        MyOrdersDal dal = new MyOrdersDal();
        Orders orders = new Orders();

        public int UserID
        {
            get { return _id; }
            set
            {

                if (value != null)
                {
                    _id = value;
                    using (dal)
                    {
                        var query = from orders in dal.Orders
                                    where orders.ID == _id
                                    select orders.UserID;

                        _id = query.First().Value;
                    }
                }
            }
        }
    }
}