using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelWebBanHang;
using System.Data.Linq;
using System.Net;
using ModelWebBanHang.DAO;
using WebShop.Code;
using System.Drawing;
using System.IO;
using System.Collections;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
       private WebBanHang db = new WebBanHang();
        public ActionResult Index()
        {

            var Menu = NhomSanPhamDAO.Instance.GetCatalogue();
            return View(Menu);
        }
        

     
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult ProductPartial(int IdGroup)
        {
           
            var ListProduct = SanPhamDAO.Instance.GetListProductByGroupAndSpecies(IdGroup);
            return PartialView(ListProduct);
        }

       

        public PartialViewResult MenuPartial()
        {

            var Menu = NhomSanPhamDAO.Instance.GetCatalogue();

            return PartialView(Menu);
        }
        public PartialViewResult SubMenuPartial(int ParentId)
        {

            var Menu = NhomSanPhamDAO.Instance.GetSubCatalogue(ParentId);
            return PartialView(Menu);
        }

        public PartialViewResult NavBarPartial()
        {
            var Menu = NhomSanPhamDAO.Instance.GetCatalogue();
            return PartialView(Menu);
        }
        [HttpGet]
        public ActionResult GroupProduct(int IdGroup,string NameGroup)
        {

            var ListProduct = SanPhamDAO.Instance.GetListProductByGroupAndSpecies(IdGroup);
            ViewBag.NameGroup = NameGroup;
            ViewBag.IdGroup = IdGroup;
            return View(ListProduct);
        }

        public String TaiKhoang()
        {
            var session = SessionHelper.GetSession();
            if(session != null)
            {
                THANH_VIEN thanhvien = db.THANH_VIEN.Find(session.UserName);
                if (thanhvien != null)
                {
                    return thanhvien.TEN_TV;
                }
                return "Quản Trị Viên";
            }
            else{
                return "Khách";
            }
            
        }



        [HttpGet]
        public ActionResult Search(string @Name)
        {

            ViewBag.KeyWord = Name;
            var ListProduct = db.SAN_PHAM.ToList().Where(p => p.TEN_SP.ConvertToUnSign().ToLower().Contains(Name.ConvertToUnSign().ToLower())).Select(p => p);
            return View(ListProduct);
        }

        public int HeaderBasket()
        {
            var Basket = Session["BasketSession"];
            var list = new List<BasketModel>();
            if (Basket != null)
            {
                list = BasketSession.GetSession();
                return list.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}