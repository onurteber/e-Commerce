using e_Commerce.Core.Model;
using e_Commerce.Core.Model.Entity;
using e_Commerce.UI.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Controllers
{
    public class HomeController : ComControllerBase
    {
        ComDB db = new ComDB();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.islogin = IsLogin;
            var model = db.Products.OrderByDescending(x => x.CreateDate).Take(7).ToList();
            return View(model);
        }

        public PartialViewResult GetMenu()
        {
            var menu = db.Categories.Where(x => x.ParentID == 0).ToList();
            return PartialView(menu);
        }

        [Route("Uye-Giris")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Uye-Giris")]
        public ActionResult Login(string Email, string Password)
        {
            var users = db.Users.Where(x => x.Email == Email && x.Password == Password &&
            x.IsActive == true && x.IsAdmin == false).ToList();
            if (users.Count == 1)
            {
                Session["LoginUSerID"] = users.FirstOrDefault().ID;
                Session["LoginUser"] = users.FirstOrDefault();
                return Redirect("/");
            }
            else 
            {
                ViewBag.Error = "Hatalı Email veya Şifre";
                return View();
            }
        }

        

        public ActionResult LogOut()
        {
            Session["LoginUSerID"] = null;
            Session["LoginUser"] = null;
            return Redirect("/Home");
        }


        [Route("uye-kayit")]
        public ActionResult CreateLogin()
        {
            return View();
        }

        [HttpPost]
        [Route("uye-kayit")]
        public ActionResult CreateLogin(User users)
        {
            try
            {
                users.CreateDate = DateTime.Now;
                users.CreateUserID = 1;
                users.IsActive = true;
                users.IsAdmin = false;
                db.Users.Add(users);
                db.SaveChanges();
                return Redirect("/");
            }
            catch (Exception ex)
            {
                return View();
            }

        }
    }
}