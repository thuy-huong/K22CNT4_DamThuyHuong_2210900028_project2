using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using K22CNT4_DamThuyHuong_2210900028_Project2.Models;

namespace K22CNT4_DamThuyHuong_2210900028_Project2.Areas.DthAdmin.Controllers
{
    public class DthDonHangsController : Controller
    {
        private DthEntities db = new DthEntities();

        // GET: DthAdmin/DthDonHangs
        public ActionResult Index()
        {
            var donHangs = db.donHangs.Include(d => d.khachHang);
            return View(donHangs.ToList());
        }

        // GET: DthAdmin/DthDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donHang donHang = db.donHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DthAdmin/DthDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.maKhachHang = new SelectList(db.khachHangs, "maKhachHang", "tenKhachHang");
            return View();
        }

        // POST: DthAdmin/DthDonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDonHang,maKhachHang,tenKhachHang,tongTien,diaChiGiaoHang,dienthoai,phiVanChuyen,phuongThucThanhToan,trangThai,ngayTao,ngayCapNhat")] donHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.donHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maKhachHang = new SelectList(db.khachHangs, "maKhachHang", "tenKhachHang", donHang.maKhachHang);
            return View(donHang);
        }

        // GET: DthAdmin/DthDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donHang donHang = db.donHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.maKhachHang = new SelectList(db.khachHangs, "maKhachHang", "tenKhachHang", donHang.maKhachHang);
            return View(donHang);
        }

        // POST: DthAdmin/DthDonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDonHang,maKhachHang,tenKhachHang,tongTien,diaChiGiaoHang,dienthoai,phiVanChuyen,phuongThucThanhToan,trangThai,ngayTao,ngayCapNhat")] donHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maKhachHang = new SelectList(db.khachHangs, "maKhachHang", "tenKhachHang", donHang.maKhachHang);
            return View(donHang);
        }

        // GET: DthAdmin/DthDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donHang donHang = db.donHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DthAdmin/DthDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            donHang donHang = db.donHangs.Find(id);
            db.donHangs.Remove(donHang);
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
