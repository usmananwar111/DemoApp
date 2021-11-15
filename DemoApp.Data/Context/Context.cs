using DemoApp.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemoApp.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=appDbConn")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}