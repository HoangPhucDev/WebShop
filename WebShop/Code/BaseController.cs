using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Code
{
    public class BaseController : Controller
    {
      protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = SessionHelper.GetSession();

            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { Controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}