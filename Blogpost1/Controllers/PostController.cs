using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blogpost1.Models;

namespace Blogpost1.Controllers
{
    public class PostController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Post
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                var addPost = db.addPost.Include(a => a.Category).Include(a => a.UserAccount);
                return View(addPost.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AddPost addPost = db.addPost.Find(id);
                if (addPost == null)
                {
                    return HttpNotFound();
                }
                return View(addPost);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.CategoryID = new SelectList(db.category, "CategoryID", "Name");
                ViewBag.UserID = new SelectList(db.userAccount, "UserID", "FirstName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,Description,Datum,UserID,CategoryID")] AddPost addPost)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    addPost.Datum = DateTime.Now;
                    addPost.UserID = Convert.ToInt32(Session["UserID"]);
                    db.addPost.Add(addPost);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CategoryID = new SelectList(db.category, "CategoryID", "Name", addPost.CategoryID);
                ViewBag.UserID = new SelectList(db.userAccount, "UserID", "FirstName", addPost.UserID);

                return View(addPost);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AddPost addPost = db.addPost.Find(id);
                if (addPost == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryID = new SelectList(db.category, "CategoryID", "Name", addPost.CategoryID);
                ViewBag.UserID = new SelectList(db.userAccount, "UserID", "FirstName", addPost.UserID);
                return View(addPost);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Title,Description,Datum,UserID,CategoryID")] AddPost addPost)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    addPost.UserID = Convert.ToInt32(Session["UserID"]);
                    db.Entry(addPost).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryID = new SelectList(db.category, "CategoryID", "Name", addPost.CategoryID);
                ViewBag.UserID = new SelectList(db.userAccount, "UserID", "FirstName", addPost.UserID);
                return View(addPost);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AddPost addPost = db.addPost.Find(id);
                if (addPost == null)
                {
                    return HttpNotFound();
                }
                return View(addPost);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                AddPost addPost = db.addPost.Find(id);
                db.addPost.Remove(addPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}