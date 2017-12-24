using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Blogpost1.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContext")
        {
        }
        public DbSet<UserAccount> userAccount { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<AddPost> addPost { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}