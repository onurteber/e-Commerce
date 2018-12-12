using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace e_Commerce.UI.WEB.Areas.Admin.Controllers
{
    public class AdminControllerBase:Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var Islogin = false;
            if(requestContext.HttpContext.Session["AdminLoginUser"] ==null)
            { //Admin Girişi Olmamışsa
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");                
            }
            else
            {
                base.Initialize(requestContext);//Admin içerdeyse sayfa çalışır.
            }
        }
    }
}