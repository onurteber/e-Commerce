using e_Commerce.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        public ComDB db = new ComDB();
        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Email,string Password)
        {
            var data = db.Users.Where(x => x.Email == Email && x.Password == Password&&
            x.IsActive==true&&x.IsAdmin==true).ToList();
            if(data.Count==1)
            {
                Session["AdminLoginUser"] = data.FirstOrDefault();
                return RedirectToAction("/index","Default");
            }
            else
            {
                return View();
            }  
        }


        public ActionResult LogOut()
        {
            Session["AdminLoginUser"] = null;
            return RedirectToAction("/Admin/AdminLogin", "Default");
        }
    }
}