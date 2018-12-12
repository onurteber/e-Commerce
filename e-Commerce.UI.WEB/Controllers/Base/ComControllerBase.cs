using e_Commerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace e_Commerce.UI.WEB
{
    public class ComControllerBase : Controller
    {
        public bool IsLogin { get; private set; }
        public int LoginUserID { get; private set; }
        public User LoginUserEntity { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["LoginUSerID"] != null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUSerID"];
                LoginUserEntity = (Core.Model.Entity.User)requestContext.HttpContext.Session["LoginUser"];
            }
            base.Initialize(requestContext);
        }
    }
}