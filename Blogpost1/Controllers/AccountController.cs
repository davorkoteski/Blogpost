using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blogpost1.Models;

namespace Blogpost1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    var usr = db.userAccount.Where(u => u.Username == account.Username || u.Email == account.Email).FirstOrDefault();
                    if (usr != null)
                    {
                        ModelState.AddModelError("", "Username or email is taken");
                    }
                    else
                    {
                        db.userAccount.Add(account);
                        db.SaveChanges();
                        ModelState.Clear();
                        ViewBag.Message = account.FirstName + " " + account.LastName + " successfully registered";
                    }
                }
            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var usr = db.userAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserID"] = usr.UserID.ToString();
                    Session["Username"] = usr.Username.ToString();
                    Session["FirstName"] = usr.FirstName.ToString();
                    Session["LastName"] = usr.LastName.ToString();
                    Session["Email"] = usr.Email.ToString();

                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is wrong");
                }
            }
            return View();
        }

        public ActionResult Profile()
        {
            using (MyDbContext db = new MyDbContext())
            {
                if (Session["UserID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
        }

        public ActionResult Logout()
        {
            if (Session["UserID"] != null)
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}