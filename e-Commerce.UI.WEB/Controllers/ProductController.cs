using e_Commerce.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Controllers
{
    public class ProductController : Controller
    {
        ComDB db = new ComDB();
        // GET: Product
        [Route("Urun/{title}/{id}")]
        public ActionResult Details(string title,int id)
        {
            var model = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return View(model);
        }
    }
}