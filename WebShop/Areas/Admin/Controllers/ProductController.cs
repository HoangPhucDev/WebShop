using ModelWebBanHang;
using ModelWebBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebShop.Code;
namespace WebShop.Areas.Admin.Controllers
{
    
    public class ProductController : BaseController
    {

        // GET: Admin/Product
        public ActionResult Index()
        {
            SanPhamDAO sanphamDAO = new SanPhamDAO();
            var sanpham = sanphamDAO.GetListProductAll();
            return View(sanpham);
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int id)
        {
            SanPhamDAO sanphamDAO = new SanPhamDAO();
            var sanpham = sanphamDAO.GetListProductById(id);
            return View(sanpham);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            //load ds sản phẩm và nhà cung ứng vào drodowbox
            var nhomsanphamDAO = new NhomSanPhamDAO();
            var nhacungcapDAO = new NhaCungCapDAO();

            ViewBag.MA_NHOM_SP = new SelectList(nhomsanphamDAO.GetCatalogueAll(), "MA_NHOM_SP", "TEN_NHOM_SP");
            ViewBag.MA_NCC = new SelectList(nhacungcapDAO.GetListSupplier(), "MA_NCC", "TEN_NCC");


            return View();
        }

        // POST: Admin/Product/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SAN_PHAM sanpham,HttpPostedFileBase fileHinhAnh)
        {
            //load ds sản phẩm và nhà cung ứng vào drodowbox
            var nhomsanphamDAO = new NhomSanPhamDAO();
            var nhacungcapDAO = new NhaCungCapDAO();

            ViewBag.MA_NHOM_SP = new SelectList(nhomsanphamDAO.GetCatalogueAll(), "MA_NHOM_SP", "TEN_NHOM_SP");
            ViewBag.MA_NCC = new SelectList(nhacungcapDAO.GetListSupplier(), "MA_NCC", "TEN_NCC");

            //kiểm tra ảnh
            var imageupload = new ImageUpload(Server.MapPath("~/Content/img"));
            if(imageupload.KiemTraThemAnhMoi(fileHinhAnh))
            {
                sanpham.HINH_ANH = imageupload.FileName;
            }else
            {
                ViewBag.ThongBao = "File Ảnh Đã Tồn Tại";
                return View();
            }

            try
            {
                
                
                int idnewproduct= SanPhamDAO.Instance.InsertProduct(sanpham);
               // fileHinhAnh.SaveAs(imageupload.PathFile);

                imageupload.SaveResizeImage(Image.FromStream(fileHinhAnh.InputStream), imageupload.PathFile);

                return RedirectToAction("Details",new {id= idnewproduct });
            }
            catch
            {
                ViewBag.ThongBao = "Thêm Thất Bại";
                return View();
            }
        }

        // GET: Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            var nhomsanphamDAO = new NhomSanPhamDAO();
            var nhacungcapDAO = new NhaCungCapDAO();
            var sanphamdao = new SanPhamDAO();

            SAN_PHAM sanpham = sanphamdao.GetListProductById(id).SingleOrDefault();

            //load ds sản phẩm và nhà cung ứng vào drodowbox
            List<SelectListItem> dsnhomsanpham = new List<SelectListItem>();
            foreach (var item in nhomsanphamDAO.GetCatalogueAll())
            {
                //load ds để chọn đúng nhóm sản phẩm
                if(item.MA_NHOM_SP==sanpham.MA_NHOM_SP)
                {
                    dsnhomsanpham.Add(new SelectListItem { Text = item.TEN_NHOM_SP, Value = item.MA_NHOM_SP.ToString(),Selected=true });
                }
                else
                {
                    dsnhomsanpham.Add(new SelectListItem { Text = item.TEN_NHOM_SP, Value = item.MA_NHOM_SP.ToString() });
                }
                
            }

            List<SelectListItem> dsnhacungcap = new List<SelectListItem>();
            foreach (var item in nhacungcapDAO.GetListSupplier())
            {
                //load ds để chọn đúng nhà cung ứng
                if (item.MA_NCC == sanpham.MA_NCC)
                {
                    dsnhacungcap.Add(new SelectListItem { Text = item.TEN_NCC, Value = item.MA_NCC.ToString(), Selected = true });
                }
                else
                {
                    dsnhacungcap.Add(new SelectListItem { Text = item.TEN_NCC, Value = item.MA_NCC.ToString() });
                }

            }



            ViewBag.MA_NHOM_SP = dsnhomsanpham;
            ViewBag.MA_NCC = dsnhacungcap;
            return View(sanpham);
        }

        // POST: Admin/Product/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, SAN_PHAM collection, HttpPostedFileBase fileHinhAnh)
        {
            var SanPhamDAO = new SanPhamDAO();
            var nhomsanphamDAO = new NhomSanPhamDAO();
            var nhacungcapDAO = new NhaCungCapDAO();
            var session = SessionHelper.GetSession();
            //load ds sản phẩm và nhà cung ứng vào drodowbox
            ViewBag.MA_NHOM_SP = new SelectList(nhomsanphamDAO.GetCatalogueAll(), "MA_NHOM_SP", "TEN_NHOM_SP");
            ViewBag.MA_NCC = new SelectList(nhacungcapDAO.GetListSupplier(), "MA_NCC", "TEN_NCC");

            //xử lý file hình
            string AnhGoc = SanPhamDAO.GetListProductById(id).Select(p => p.HINH_ANH).SingleOrDefault();
            var imageupload = new ImageUpload(Server.MapPath("~/Content/img"));
            if (imageupload.KiemTraSuaAnh(fileHinhAnh, AnhGoc))
            {
                collection.HINH_ANH = imageupload.FileName;
            }
            else
            {
                ViewBag.ThongBao = "File Ảnh Đã Tồn Tại";
                return View();
            }



           

            try
            {
                collection.NGAY_SUA = DateTime.Today;
                collection.NGUOI_SUA = session.UserName;

                SanPhamDAO.Instance.UpdateProduct(collection);
                if (imageupload.PathFile != null)
                {
                   // fileHinhAnh.SaveAs(imageupload.PathFile);

                    imageupload.SaveResizeImage(Image.FromStream(fileHinhAnh.InputStream), imageupload.PathFile);
                }
                ViewBag.ThongBao = "Sửa Thành Công";
                return RedirectToAction("Details", new { id = id });
            }
            catch
            {
                ViewBag.ThongBao = "Sửa Thất Bại";
                return View();
            }
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int id)
        {
            SanPhamDAO.Instance.DeletetProduct(id);
            return RedirectToAction("Index");
        }

        // POST: Admin/Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
