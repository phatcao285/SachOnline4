using SachOnline4.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SachOnline4.Controllers
{
    public class SachOnlinesController : Controller
    {
        private SachOnlineEntities db = new SachOnlineEntities();

        // GET: SachOnlines
        public ActionResult Index()
        {
            var sACHes = db.SACHes
                 .Include(s => s.CHUDE) // Đảm bảo rằng 'CHUDE' là một navigation property hợp lệ trong bảng 'SACHes'
                 .Include(s => s.NHAXUATBAN) // Đảm bảo rằng 'NHAXUATBAN' cũng là một navigation property hợp lệ trong bảng 'SACHes'
                 .Take(8) // Lấy tối đa 8 bản ghi
                 .ToList(); // Chuyển kết quả thành danh sách

            return View(sACHes.ToList());

        }

        public ActionResult ChuDePartials()
        {
            var chuDeList = db.CHUDEs.ToList();
            return PartialView(chuDeList);
        }

        public ActionResult NXBPartials()
        {
            var NBXList = db.NHAXUATBANs.ToList();
            return PartialView(NBXList);
        }

        public ActionResult SachBNPartials()
        {
          var topSach = db.SACHes
         .OrderByDescending(s => s.CHITIETDATHANGs.Sum(ctdh => ctdh.SoLuong))
         .Take(6)
         .ToList();

            return PartialView(topSach);
        }


        // GET: SachOnlines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        public ActionResult SachTheoChuDe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm chủ đề theo ID
            CHUDE chuDe = db.CHUDEs.Find(id);

            if (chuDe == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách sách thuộc chủ đề
            var sachTheoChuDe = db.SACHes.Where(s => s.MaCD == id).ToList();

            // Truyền dữ liệu chủ đề và danh sách sách vào view
            ViewBag.ChuDe = chuDe;
            return View(sachTheoChuDe);
        }

        public ActionResult SachTheoNXB(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Tìm chủ đề theo ID
            NHAXUATBAN nHAXUATBAN = db.NHAXUATBANs.Find(id);

            if (nHAXUATBAN == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách sách thuộc chủ đề
            var sachTheoNhaXuatBan = db.SACHes.Where(s => s.MaNXB == id).ToList();

            // Truyền dữ liệu chủ đề và danh sách sách vào view
            ViewBag.nhaXuatBan = nHAXUATBAN;
            return View(sachTheoNhaXuatBan);
        }

        // GET: SachOnlines/Create
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB");
            return View();
        }

        // POST: SachOnlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,MoTa,AnhBia,NgayCapNhat,SoLuongBan,GiaBan,MaCD,MaNXB")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.SACHes.Add(sACH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: SachOnlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // POST: SachOnlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,MoTa,AnhBia,NgayCapNhat,SoLuongBan,GiaBan,MaCD,MaNXB")] SACH sACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs, "MaCD", "TenChuDe", sACH.MaCD);
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs, "MaNXB", "TenNXB", sACH.MaNXB);
            return View(sACH);
        }

        // GET: SachOnlines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SACH sACH = db.SACHes.Find(id);
            if (sACH == null)
            {
                return HttpNotFound();
            }
            return View(sACH);
        }

        // POST: SachOnlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SACH sACH = db.SACHes.Find(id);
            db.SACHes.Remove(sACH);
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
