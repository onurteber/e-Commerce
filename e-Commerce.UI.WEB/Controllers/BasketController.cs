using e_Commerce.Core.Model;
using e_Commerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Controllers
{
    public class BasketController : ComControllerBase
    {
        ComDB db = new ComDB();
        // GET: Basket
        public JsonResult AddProduct(int productID,int quantity)
        {
            db.Baskets.Add(new Core.Model.Entity.Basket
            {
                CreateDate = DateTime.Now,
                CreateUserID=LoginUserID,
                ProductID=productID,
                Quantity=quantity,
                UserID=LoginUserID
            });
            var data=db.SaveChanges();
            return Json(data,JsonRequestBehavior.AllowGet);
        }


        [Route("Sepetim")]
        public ActionResult Index()
        {
            var data = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }


        public ActionResult Delete(int id)
        {
            Basket bas = db.Baskets.Find(id);
            db.Baskets.Remove(bas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}