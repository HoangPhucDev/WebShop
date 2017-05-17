using ModelWebBanHang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Code;

namespace WebShop.Controllers
{
    public class CheckOutController : Controller
    {
        private WebBanHang db = new WebBanHang();

        public ActionResult Index()
        {

            var session = BasketSession.GetSession();
            if (session != null)
            {
                ViewBag.TongTien = 0;
                foreach (var item in session)
                {
                    ViewBag.TongTien += (decimal)(item.Gia * item.SoLuong);
                }

                if (SessionHelper.GetSession() != null)
                {
                    THANH_VIEN user = db.THANH_VIEN.ToList().Where(p => p.MA_TV == SessionHelper.GetSession().UserName).Single();
                    KHACH_HANG customer = new KHACH_HANG
                    {
                        TEN_KH = user.TEN_TV,
                        LIEN_HE = user.LIEN_HE,
                        Email = user.Email,
                        DIA_CHI = user.DIA_CHI
                    };
                    return View(new HOA_DON {KHACH_HANG = customer});
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error", "Basket");
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            if (BasketSession.GetSession() != null && ModelState.IsValid)
            {
                ViewBag.TongTien = decimal.Parse(form.Get("TongTien"));

                if (SessionHelper.GetSession() != null)
                {
                    THANH_VIEN user = db.THANH_VIEN.ToList().Where(p => p.MA_TV == SessionHelper.GetSession().UserName).Single();
                    KHACH_HANG customer = new KHACH_HANG
                    {
                        TEN_KH = user.TEN_TV,
                        LIEN_HE = user.LIEN_HE,
                        Email = user.Email,
                        DIA_CHI = user.DIA_CHI
                    };
                    return View(new HOA_DON { KHACH_HANG = customer });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Error", "Basket");
            }
        }

        [HttpPost]
        public ActionResult Checkout1(FormCollection form, HOA_DON Order)
        {
            var session = BasketSession.GetSession();
            if (session != null && form.Get("KHACH_HANG.TEN_KH") != "" && form.Get("KHACH_HANG.LIEN_HE") != "")
            {
                List<SAN_PHAM> ListBasket = new List<SAN_PHAM>();
                ViewBag.TongTien = 0;
                ViewBag.Order = Order;
                foreach (var item in session)
                {
                    ListBasket.Add(new SAN_PHAM
                    {
                        TEN_SP = item.TenSP,
                        MA_SP = item.MaSP,
                        GIA_BAN = item.Gia,
                        HINH_ANH = db.SAN_PHAM.Where(p => p.MA_SP == item.MaSP).Select(p => p.HINH_ANH).Single(),
                        SO_LUONG = item.SoLuong
                    });
                    ViewBag.TongTien += (decimal)(item.Gia * item.SoLuong);
                }
                return View(ListBasket);
            }
            else
            {
                if (form.Get("KHACH_HANG.TEN_KH") == "" || form.Get("KHACH_HANG.LIEN_HE") == "" || form.Get("KHACH_HANG.DIA_CHI") == "" || form.Get("KHACH_HANG.TINH") == "")
                    return RedirectToAction("Index", "CheckOut");
                else
                    return RedirectToAction("Error", "Basket");
            }
        }
        [HttpPost]
        public ActionResult Checkout(FormCollection form, HOA_DON Order)
        {
            var session = BasketSession.GetSession();
            if (session != null)
            {

                int MaHD = db.HOA_DON.Max(p => p.MA_HD) + 1;
                int MaKH = db.KHACH_HANG.Max(p => p.MA_KH) + 1;

                Order.KHACH_HANG.MA_TV = "demo";
                Order.KHACH_HANG.MA_KH = MaKH;
                Order.KHACH_HANG.GIOI_TINH = true;
                Order.KHACH_HANG.NGAY_SINH = DateTime.Now;

                Order.MA_HD = MaHD;
                Order.NGAY_LAP = DateTime.Now;
                Order.TRANG_THAI = 0;
                Order.MA_KH = MaKH;
                Order.GHI_CHU = "Chưa Xem";

                if (SessionHelper.GetSession() != null)
                {
                    var user = SessionHelper.GetSession();
                    Order.KHACH_HANG.MA_TV = user.UserName;
                }

                var res = InsertCustomer(Order.KHACH_HANG);
                if (res)
                {
                    db.HOA_DON.Add(Order);
                    foreach (var item in session)
                    {
                        db.CHITIET_HD.Add(new CHITIET_HD { MA_HD = MaHD, SO_LUONG_SP = item.SoLuong, MA_SP = item.MaSP, TONG_GIA = (decimal)(item.SoLuong * item.Gia) });
                    }
                    db.SaveChanges();
                    BasketSession.RemoveSession();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Error", "CheckOut");
                }
            }
            else
            {
                return RedirectToAction("Error", "Basket");
            }
        }

        public bool InsertCustomer(KHACH_HANG Customer)
        {
            db.KHACH_HANG.Add(Customer);
            int ressave = db.SaveChanges();

            return ressave > 0 ? true : false;
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}