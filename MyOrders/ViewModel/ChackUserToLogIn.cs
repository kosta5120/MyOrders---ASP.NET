using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyOrders.Dal;
using MyOrders.Models;

namespace MyOrders.ViewModel
{
    public class ChackUserToLogIn
    {
        public bool CheckUser(MyOrdersDal dal, string pass, string email, out string id)
        {
            id = string.Empty;

            bool status = dal.Users.ToList().Exists(model => model.Password.Equals(pass, StringComparison.CurrentCultureIgnoreCase) && model.EmailAddress.Equals(email, StringComparison.CurrentCultureIgnoreCase));

            if(status)
            {
                using (var con = dal)
                {
                    var query = from _id in con.Users
                                where _id.EmailAddress == email && _id.Password == pass
                                select _id.ID;
                    var t = con.Users
                        .Where(model => model.EmailAddress == email && model.Password == pass).Select(model => new { id = model.ID }).FirstOrDefault();
                    id = t.id.ToString();
                }
            }
           
            return status;
        }
    }
}