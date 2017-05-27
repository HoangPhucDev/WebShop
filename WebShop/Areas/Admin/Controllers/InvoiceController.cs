using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelWebBanHang;

namespace WebShop.Areas.Admin.Controllers
{
    public class InvoiceController : Controller
    {
        private WebBanHang db = new WebBanHang();

        // GET: Admin/Invoice
        public ActionResult Index()
        {
            var hOA_DON = db.HOA_DON.Include(h => h.KHACH_HANG).Include(h => h.NHAN_VIEN);
            return View(hOA_DON.ToList());
        }

        // GET: Admin/Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Order = db.HOA_DON.Find(id);
            if (db.HOA_DON.Find(id) == null)
            {
                return HttpNotFound();
            }
            
            List<CHITIET_HD> hOA_DON = db.CHITIET_HD.Where(p => p.MA_HD == id).ToList();
            
            return View(hOA_DON);
        }

        // GET: Admin/Invoice/Create
        public ActionResult Create()
        {
            ViewBag.MA_KH = new SelectList(db.KHACH_HANG, "MA_KH", "MA_TV");
            ViewBag.MA_NV = new SelectList(db.NHAN_VIEN, "MA_NV", "MAT_KHAU");
            return View();
        }

        // POST: Admin/Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_HD,MA_KH,MA_NV,NGAY_LAP,TINH_GIAO_HANG,DIA_CHI_GIAO_HANG,GHI_CHU,PHI_GIAO_HANG,THANH_TIEN,TRANG_THAI")] HOA_DON hOA_DON)
        {
            if (ModelState.IsValid)
            {
                db.HOA_DON.Add(hOA_DON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_KH = new SelectList(db.KHACH_HANG, "MA_KH", "MA_TV", hOA_DON.MA_KH);
            ViewBag.MA_NV = new SelectList(db.NHAN_VIEN, "MA_NV", "MAT_KHAU", hOA_DON.MA_NV);
            return View(hOA_DON);
        }

        // GET: Admin/Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOA_DON hOA_DON = db.HOA_DON.Find(id);
            if (hOA_DON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_KH = new SelectList(db.KHACH_HANG, "MA_KH", "MA_TV", hOA_DON.MA_KH);
            ViewBag.MA_NV = new SelectList(db.NHAN_VIEN, "MA_NV", "MAT_KHAU", hOA_DON.MA_NV);
            return View(hOA_DON);
        }

        // POST: Admin/Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_HD,MA_KH,MA_NV,NGAY_LAP,TINH_GIAO_HANG,DIA_CHI_GIAO_HANG,GHI_CHU,PHI_GIAO_HANG,THANH_TIEN,TRANG_THAI")] HOA_DON hOA_DON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOA_DON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_KH = new SelectList(db.KHACH_HANG, "MA_KH", "MA_TV", hOA_DON.MA_KH);
            ViewBag.MA_NV = new SelectList(db.NHAN_VIEN, "MA_NV", "MAT_KHAU", hOA_DON.MA_NV);
            return View(hOA_DON);
        }

        // GET: Admin/Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOA_DON hOA_DON = db.HOA_DON.Find(id);
            if (hOA_DON == null)
            {
                return HttpNotFound();
            }
            return View(hOA_DON);
        }

        // POST: Admin/Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HOA_DON hOA_DON = db.HOA_DON.Find(id);
            db.HOA_DON.Remove(hOA_DON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
