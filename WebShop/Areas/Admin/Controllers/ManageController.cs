using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Code;

namespace WebShop.Areas.Admin.Controllers
{
    public class ManageController : BaseController
    {
        // GET: Admin/Manage
        public ActionResult Index()
        {
            return View();
        }

        public String TaiKhoang()
        {
            var session = SessionHelper.GetSession();
            return session.UserName;
        }

        public ActionResult Logout()
        {
            SessionHelper.RemoveSession();
            return RedirectToAction("Index","Login");
        }
    }
}