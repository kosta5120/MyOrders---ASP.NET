using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MyOrders.Models;

namespace MyOrders.Dal
{
    public class MyOrdersDal :DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<login_Signup>().ToTable("LogIn/SignUp");
        }
        public DbSet<login_Signup> Users { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}