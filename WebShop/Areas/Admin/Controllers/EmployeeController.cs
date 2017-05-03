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
    public class EmployeeController : Controller
    {
        private WebBanHang db = new WebBanHang();

        // GET: Admin/Employee
        public ActionResult Index()
        {
            return View(db.NHAN_VIEN.ToList());
        }

        // GET: Admin/Employee/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // GET: Admin/Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_NV,MAT_KHAU,TEN_NV,CHUC_VU,LIEN_HE,Email")] NHAN_VIEN nHAN_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.NHAN_VIEN.Add(nHAN_VIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHAN_VIEN);
        }

        // GET: Admin/Employee/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_NV,MAT_KHAU,TEN_NV,CHUC_VU,LIEN_HE,Email")] NHAN_VIEN nHAN_VIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAN_VIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAN_VIEN);
        }

        // GET: Admin/Employee/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            if (nHAN_VIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHAN_VIEN);
        }

        // POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHAN_VIEN nHAN_VIEN = db.NHAN_VIEN.Find(id);
            db.NHAN_VIEN.Remove(nHAN_VIEN);
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
