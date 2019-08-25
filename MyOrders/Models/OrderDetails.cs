using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyOrders.Models
{
    public class OrderDetails
    {
        public string  WebNmae { get; set; }
        public long OrderID { get; set; }
        public string DateTime { get; set; }
        public string Price { get; set; }
        public string TrackNumber { get; set; }
    }
}