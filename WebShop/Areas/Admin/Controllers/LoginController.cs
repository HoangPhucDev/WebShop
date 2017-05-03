using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Areas.Admin.Models;
using ModelWebBanHang.DAO;
using WebShop.Code;

namespace WebShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login

        [ValidateInput(true)]
        public ActionResult Index()
        {
            if(SessionHelper.GetSession()!=null)
            {
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            var res = NhanVienDAO.Instance.CheckLogin(model.Username, model.Password);
            if (res && ModelState.IsValid)
            {
                SessionHelper.SetSession(new UserSession() { UserName = model.Username,RememberMe = model.RememberMe });
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                ModelState.AddModelError("", "Sai tài khoảng Hoặc mật khẩu");
            }

            return View(model);
        }
    }
}