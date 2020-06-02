using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyOrders.Models
{
    public class Orders
    {
        public int ID { get; set; }
        public int UserID{get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOrder { get; set; }
        public double Cost { get; set; }
        public string WebName { get; set; }
        public string TrackingNumber { get; set; }
        public  byte Provided { get; set; }
    }
}
