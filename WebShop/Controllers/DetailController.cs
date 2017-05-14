using ModelWebBanHang;
using ModelWebBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Controllers
{

    public class DetailController : Controller
    {

        private WebBanHang db = new WebBanHang();
        // GET: Detail
        public ActionResult Index(string id)
        {
            SanPhamDAO sanphamdao = new SanPhamDAO();
            var Product = sanphamdao.GetListProductById(Convert.ToInt32(id));
            if (id == null)
            {
                return HttpNotFound(); //dùng hàm sao để trả về trang bad new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return View(Product);
            }
            
        }
    }
}