using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Blogpost1.Models;

namespace Blogpost1.Controllers
{
    public class ViewPostsController : ApiController
    {
        private MyDbContext db = new MyDbContext();

        // GET: api/ViewPosts
        public IEnumerable<Category> Getcategory()
        {
            return db.category;
        }

        // GET: api/ViewPosts/5
        public IHttpActionResult GetCategory(int id)
        {

            var addpost = db.addPost.Where(u => u.CategoryID == id).Select(u=>new { CategoryID = u.CategoryID, Category = u.Category, Datum = u.Datum, Description = u.Description, PostID = u.PostID, Title = u.Title, UserAccount = u.UserAccount, UserID = u.UserID }).ToArrayAsync();
            
            if (addpost == null)
            {
                return NotFound();
            }

            return Ok(addpost);
        }
    }
}