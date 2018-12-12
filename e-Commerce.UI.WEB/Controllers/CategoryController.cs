using e_Commerce.Core.Model;
using e_Commerce.UI.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Controllers
{
    public class CategoryController : ComControllerBase
    {
        // GET: Category
        ComDB db = new ComDB();

            [Route("Kategori/{isim}/{id}")]
        public ActionResult Index(string isim,int id)
        {
            ViewBag.islogin = IsLogin;
            var model = db.Products.Where(x => x.CategoryID == id&&x.IsActive==true).ToList();
            ViewBag.category = db.Categories.Where(x => x.ID == id).FirstOrDefault();
            return View(model);
        }
    }
}