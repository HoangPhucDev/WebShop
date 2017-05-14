using ModelWebBanHang;
using ModelWebBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Code;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class BasketController : Controller
    {
        private WebBanHang db = new WebBanHang();
        // GET: Basket
        public ActionResult Index()
        {
            var session = BasketSession.GetSession();

            if (session != null)
            {
                List<SAN_PHAM> ListBasket = new List<SAN_PHAM>();
                ViewBag.TongTien = 0;
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
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public ActionResult AddBasket(int Id, int Quantity)
        {
            var Product = SanPhamDAO.Instance.GetListProductById(Id);

            BasketSession.AddSession(new BasketModel() { MaSP = Product.MA_SP, TenSP = Product.TEN_SP, Gia = Product.GIA_BAN, SoLuong = Quantity });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AddBasket(int Id)
        {
            var Product = SanPhamDAO.Instance.GetListProductById(Id);

            BasketSession.AddSession(new BasketModel() { MaSP = Product.MA_SP, TenSP = Product.TEN_SP, Gia = Product.GIA_BAN, SoLuong = 1 });
            return RedirectToAction("Index", "Home");
        }

    }
}