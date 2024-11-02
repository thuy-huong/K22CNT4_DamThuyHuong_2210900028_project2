using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using K22CNT4_DamThuyHuong_2210900028_Project2.Models;

namespace K22CNT4_DamThuyHuong_2210900028_Project2.Areas.DthAdmin.Controllers
{
    public class DthSanPhamsController : Controller
    {
        private DthEntities db = new DthEntities();

        // GET: DthAdmin/DthSanPhams
        public ActionResult DthIndex()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");
            }
            var sanPhams = db.sanPhams.Include(s => s.danhMuc);
            return View(sanPhams.ToList());
        }

        // GET: DthAdmin/DthSanPhams/Details/5
        public ActionResult DthDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: DthAdmin/DthSanPhams/Create
        public ActionResult DthCreate()
        {
            ViewBag.maDanhMuc = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc");
            return View();
        }

        // POST: DthAdmin/DthSanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DthCreate(sanPham sanPham, HttpPostedFileBase file)
        {
            var existingProduct = db.sanPhams.FirstOrDefault(p => p.tenSanPham == sanPham.tenSanPham);
            if (existingProduct != null)
            {
                ModelState.AddModelError("tenSanPham", "Sản phẩm đã tồn tại. Vui lòng nhập sản phẩm khác.");
            }

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Public/UpLoadImg/imgSanPham"), fileName);
                    file.SaveAs(path);

                    sanPham.hinhAnh = fileName;
                }
                db.sanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DthIndex");
            }
            else { ModelState.AddModelError("loi", "Lỗi thêm mới"); }
            ViewBag.maDanhMuc = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return View(sanPham);
        }
        
        // GET: DthAdmin/DthSanPhams/Edit/5
        public ActionResult DthEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.maDanhMuc = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return View(sanPham);
        }

        // POST: DthAdmin/DthSanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DthEdit(sanPham sanPham, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Public/UpLoadImg/imgSanPham"), fileName);
                    file.SaveAs(path);

                    sanPham.hinhAnh = fileName;
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DthIndex");
            }
            ViewBag.maDanhMuc = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", sanPham.maDanhMuc);
            return View(sanPham);
        }

        // GET: DthAdmin/DthSanPhams/Delete/5
        public ActionResult DthDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: DthAdmin/DthSanPhams/Delete/5
        [HttpPost, ActionName("DthDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sanPham sanPham = db.sanPhams.Find(id);
            db.sanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("DthIndex");
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
