using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Globalization;

namespace MyOrders.Models
{
    public class OrderDetailsModel
    {
        [Display(Name = "Web Order")]
        public string  WebNmae { get; set; }
        public long OrderID { get; set; }
        public string DateTime { get; set; }
        public string Price { get; set; }
        public Currency Currency { get; set; }
        public string TrackNumber { get; set; }
        public string PassedTime { get; set; }

    }

    public enum Currency
    {
        USA,
        NIS
    }


    
}