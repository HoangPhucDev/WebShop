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
    public class CustomerController : Controller
    {
        private WebBanHang db = new WebBanHang();

        // GET: Admin/Customer
        public ActionResult Index()
        {
            var kHACH_HANG = db.KHACH_HANG.Include(k => k.THANH_VIEN);
            return View(kHACH_HANG.ToList());
        }

        // GET: Admin/Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACH_HANG);
        }

        // GET: Admin/Customer/Create
        public ActionResult Create()
        {
            ViewBag.MA_TV = new SelectList(db.THANH_VIEN, "MA_TV", "MAT_KHAU");
            return View();
        }

        // POST: Admin/Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_KH,MA_TV,TEN_KH,GIOI_TINH,NGAY_SINH,TINH,DIA_CHI,LIEN_HE,Email")] KHACH_HANG kHACH_HANG)
        {
            if (ModelState.IsValid)
            {
                db.KHACH_HANG.Add(kHACH_HANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MA_TV = new SelectList(db.THANH_VIEN, "MA_TV", "MAT_KHAU", kHACH_HANG.MA_TV);
            return View(kHACH_HANG);
        }

        // GET: Admin/Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MA_TV = new SelectList(db.THANH_VIEN, "MA_TV", "MAT_KHAU", kHACH_HANG.MA_TV);
            return View(kHACH_HANG);
        }

        // POST: Admin/Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_KH,MA_TV,TEN_KH,GIOI_TINH,NGAY_SINH,TINH,DIA_CHI,LIEN_HE,Email")] KHACH_HANG kHACH_HANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACH_HANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MA_TV = new SelectList(db.THANH_VIEN, "MA_TV", "MAT_KHAU", kHACH_HANG.MA_TV);
            return View(kHACH_HANG);
        }

        // GET: Admin/Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            if (kHACH_HANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACH_HANG);
        }

        // POST: Admin/Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHACH_HANG kHACH_HANG = db.KHACH_HANG.Find(id);
            db.KHACH_HANG.Remove(kHACH_HANG);
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
