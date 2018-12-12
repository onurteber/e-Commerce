using e_Commerce.Core.Model;
using e_Commerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_Commerce.UI.WEB.Controllers
{
    public class OrderController : ComControllerBase
    {
        // GET: Order
        ComDB db = new ComDB();

        [Route("Siparisver")]
        public ActionResult AddresList()
        {
            var data = db.UserAdress.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult CreateUserAdress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserAdress(UserAdress userAdress)
        {
            userAdress.CreateDate = DateTime.Now;
            userAdress.CreateUserID = LoginUserID;
            userAdress.IsActive = true;
            userAdress.UserID = LoginUserID;
            db.UserAdress.Add(userAdress);
            db.SaveChanges();
            return RedirectToAction("AddresList");
        }

        public ActionResult CreateOrder(int id)
        {
            var sepet = db.Baskets.Include("Product").Where(x => x.UserID == LoginUserID).ToList();
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserID = LoginUserID;
            order.StatusID = 1;
            order.TotalProductPrice = sepet.Sum(x => x.Product.Price);
            order.TotalTaxPrice = sepet.Sum(x => x.Product.Tax);
            order.TotalDiscount = sepet.Sum(x => x.Product.Discount);
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice - order.TotalDiscount;
            order.UserAdressID = id;
            order.UserID = LoginUserID;
            order.OrderProducts = new List<OrderProduct>();

            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserID = LoginUserID,
                    ProductID = item.ProductID,
                    Quantity = item.Quantity

                });
                db.Baskets.Remove(item);
            }
            db.Orders.Add(order);
            db.SaveChanges();
            var orderid = db.Orders.Where(x => x.UserID == LoginUserID).ToList().LastOrDefault().ID;
            return RedirectToAction("Details",new { id=orderid});
        }

        public ActionResult Details(int id)
        {
            var data = db.Orders.Include("OrderProducts").Include("OrderProducts.Product").
                Include("OrderPayments").Include("Status").Include("UserAdress").Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }


        [Route("Siparislerim")]
        public ActionResult Index()
        {
            var data = db.Orders.Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }

        public ActionResult Pay(int id)
        {
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 7;
            db.SaveChanges();
            return RedirectToAction("Details",new { id=order.ID});
        }
    }
}
