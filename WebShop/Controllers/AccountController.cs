using ModelWebBanHang;
using ModelWebBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebShop.Code;
using System.Linq;

namespace WebShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        private WebBanHang db = new WebBanHang();
        [ValidateInput(true)]
        public ActionResult Index()
        {
            if (SessionHelper.GetSession() != null)
            {
                var session = SessionHelper.GetSession();
                THANH_VIEN list = db.THANH_VIEN.Find(session.UserName);
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(String Username, String Password, bool RememberMe)
        {
            var res = ThanhVienDAO.Instance.CheckLogin(Username, Password);
            if (res && ModelState.IsValid)
            {
                SessionHelper.SetSession(new UserSession() { UserName = Username, RememberMe = RememberMe });
                var session = SessionHelper.GetSession();
                THANH_VIEN list = db.THANH_VIEN.Find(session.UserName);
                return View(list);
            }
            else
            {
                ModelState.AddModelError("", "Sai tài khoảng Hoặc mật khẩu");
            }
            return RedirectToAction("Index", "Home");


        }

        public ActionResult Order()
        {
            var session = SessionHelper.GetSession();
            List<HOA_DON> hd = db.HOA_DON.Where(p => p.KHACH_HANG.THANH_VIEN.MA_TV == session.UserName).ToList();
            return View(hd);
        }

        public ActionResult DetailOrder(int IDOrder)
        {
            ViewBag.Order = db.HOA_DON.Where(p => p.MA_HD == IDOrder).Single();
            List<CHITIET_HD> hd = db.CHITIET_HD.Where(p => p.MA_HD== IDOrder).ToList();
            return View(hd);
        }
        public ActionResult Logout()
        {
            SessionHelper.RemoveSession();
            return RedirectToAction("Index", "Home");
        }
    }
}