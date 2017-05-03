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
    public class CategoryController : Controller
    {
        private WebBanHang db = new WebBanHang();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.NHOM_SAN_PHAM.ToList());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_SAN_PHAM nHOM_SAN_PHAM = db.NHOM_SAN_PHAM.Find(id);
            if (nHOM_SAN_PHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOM_SAN_PHAM);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MA_NHOM_SP,TEN_NHOM_SP,Order,TRANG_THAI,MA_NHOM_CHA")] NHOM_SAN_PHAM nHOM_SAN_PHAM)
        {
            if (ModelState.IsValid)
            {
                db.NHOM_SAN_PHAM.Add(nHOM_SAN_PHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHOM_SAN_PHAM);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_SAN_PHAM nHOM_SAN_PHAM = db.NHOM_SAN_PHAM.Find(id);
            if (nHOM_SAN_PHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOM_SAN_PHAM);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MA_NHOM_SP,TEN_NHOM_SP,Order,TRANG_THAI,MA_NHOM_CHA")] NHOM_SAN_PHAM nHOM_SAN_PHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHOM_SAN_PHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHOM_SAN_PHAM);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOM_SAN_PHAM nHOM_SAN_PHAM = db.NHOM_SAN_PHAM.Find(id);
            if (nHOM_SAN_PHAM == null)
            {
                return HttpNotFound();
            }
            return View(nHOM_SAN_PHAM);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHOM_SAN_PHAM nHOM_SAN_PHAM = db.NHOM_SAN_PHAM.Find(id);
            db.NHOM_SAN_PHAM.Remove(nHOM_SAN_PHAM);
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
