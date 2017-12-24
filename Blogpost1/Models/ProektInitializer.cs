using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Blogpost1.Models
{
    public class ProektInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            var category = new List<Category>
            {
            new Category{Name="Vesti"},
            new Category{Name="Sport"},
            new Category{Name="Zivot"},
            new Category{Name="Zabava"},
            new Category{Name="Tehnologija"},
            };

            category.ForEach(s => context.category.Add(s));
            context.SaveChanges();
        }
    }
}