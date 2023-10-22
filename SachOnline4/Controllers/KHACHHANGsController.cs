using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using SachOnline4.Models;

namespace SachOnline4.Controllers
{
    public class KHACHHANGsController : Controller
    {
        private SachOnlineEntities db = new SachOnlineEntities();

        // GET: KHACHHANGs
        public ActionResult Index()
        {
            return View(db.KHACHHANGs.ToList());
        }

        // GET: KHACHHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // GET: KHACHHANGs/Create
        public ActionResult Create()
        {
            KHACHHANG kHACHHANG = new KHACHHANG();
            return View(kHACHHANG);
        }
        public bool IsEmailValid(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, pattern);
        }

        // POST: KHACHHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,HoTen,TaiKhoan,MatKhau,Email,DiaChi,DienThoai,NgaySinh,ConfirmPassword")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(kHACHHANG.HoTen))
                {
                    ModelState.AddModelError("HoTen", "Họ tên không được để trống");
                }

                if (string.IsNullOrEmpty(kHACHHANG.TaiKhoan))
                {
                    ModelState.AddModelError("TaiKhoan", "Tên đăng nhập không được để trống");
                }

                if (string.IsNullOrEmpty(kHACHHANG.MatKhau))
                {
                    ModelState.AddModelError("MatKhau", "Mật khẩu không được để trống");
                }

                if (!string.Equals(kHACHHANG.MatKhau, kHACHHANG.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu nhập lại không khớp với mật khẩu đã nhập");
                }

                if (string.IsNullOrEmpty(kHACHHANG.Email))
                {
                    ModelState.AddModelError("Email", "Email không được để trống");
                }

                else if (!IsEmailValid(kHACHHANG.Email))
                {
                    ModelState.AddModelError("Email", "Email không hợp lệ");
                }

                if (string.IsNullOrEmpty(kHACHHANG.DiaChi))
                {
                    ModelState.AddModelError("DiaChi", "Địa chỉ không được để trống");
                }

                if (string.IsNullOrEmpty(kHACHHANG.DienThoai))
                {
                    ModelState.AddModelError("DienThoai", "Điện thoại không được để trống");
                }

                if (ModelState.IsValid)
                {
                    db.KHACHHANGs.Add(kHACHHANG);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(kHACHHANG);
        }


        public ActionResult Login([Bind(Include = "TaiKhoan, MatKhau")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                // Thực hiện kiểm tra thông tin đăng nhập
                var user = db.KHACHHANGs.SingleOrDefault(u => u.TaiKhoan == kHACHHANG.TaiKhoan && u.MatKhau == kHACHHANG.MatKhau);

                if (user != null)
                {
                    Session["TaiKhoan"] = user;
                    return RedirectToAction("Index", "SachOnlines");
                }
                else
                {
                    ViewBag.LoginFailed = true;
                    ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không hợp lệ.");
                }
            }

            return View(kHACHHANG);
        }
    

        // GET: KHACHHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KHACHHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,HoTen,TaiKhoan,MatKhau,Email,DiaChi,DienThoai,NgaySinh")] KHACHHANG kHACHHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHACHHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHACHHANG);
        }

        // GET: KHACHHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            if (kHACHHANG == null)
            {
                return HttpNotFound();
            }
            return View(kHACHHANG);
        }

        // POST: KHACHHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHACHHANG kHACHHANG = db.KHACHHANGs.Find(id);
            db.KHACHHANGs.Remove(kHACHHANG);
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
