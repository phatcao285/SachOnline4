using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SachOnline4.Models;

namespace SachOnline4.Controllers
{
    public class NhaXuatBanController : Controller
    {
        private SachOnlineEntities db = new SachOnlineEntities();

        // GET: NhaXuatBan
        public ActionResult Index()
        {
            return View(db.NHAXUATBANs.ToList());
        }

        public ActionResult ChuDePartial()
        {
            return View(db.CHUDEs.ToList());
        }

        // GET: NhaXuatBan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // GET: NhaXuatBan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NhaXuatBan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNXB,TenNXB,DiaChi,DienThoai")] NHAXUATBAN nHAXUATBAN)
        {
            if (ModelState.IsValid)
            {
                db.NHAXUATBANs.Add(nHAXUATBAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHAXUATBAN);
        }

        // GET: NhaXuatBan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // POST: NhaXuatBan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNXB,TenNXB,DiaChi,DienThoai")] NHAXUATBAN nHAXUATBAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHAXUATBAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHAXUATBAN);
        }

        // GET: NhaXuatBan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }
            return View(nHAXUATBAN);
        }

        // POST: NhaXuatBan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);
            db.NHAXUATBANs.Remove(nHAXUATBAN);
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
