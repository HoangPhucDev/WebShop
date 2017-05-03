using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ModelWebBanHang;

namespace WebShop.Areas.Admin
{
    public class MemberController : Controller
    {
        private WebBanHang db = new WebBanHang();

        // GET: Admin/Member
        public ActionResult Index()
        {
            return View(db.THANH_VIEN.ToList());
        }

        // GET: Admin/Member/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANH_VIEN tHANH_VIEN = db.THANH_VIEN.Find(id);
            if (tHANH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(tHANH_VIEN);
        }

        // GET: Admin/Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_TV,MAT_KHAU,TEN_TV,GIOI_TINH,NGAY_SINH,TINH,DIA_CHI,LIEN_HE,Email")] THANH_VIEN tHANH_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.THANH_VIEN.Add(tHANH_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tHANH_VIEN);
        }

        // GET: Admin/Member/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANH_VIEN tHANH_VIEN = db.THANH_VIEN.Find(id);
            if (tHANH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(tHANH_VIEN);
        }

        // POST: Admin/Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_TV,MAT_KHAU,TEN_TV,GIOI_TINH,NGAY_SINH,TINH,DIA_CHI,LIEN_HE,Email")] THANH_VIEN tHANH_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHANH_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHANH_VIEN);
        }

        // GET: Admin/Member/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANH_VIEN tHANH_VIEN = db.THANH_VIEN.Find(id);
            if (tHANH_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(tHANH_VIEN);
        }

        // POST: Admin/Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            THANH_VIEN tHANH_VIEN = db.THANH_VIEN.Find(id);
            db.THANH_VIEN.Remove(tHANH_VIEN);
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
