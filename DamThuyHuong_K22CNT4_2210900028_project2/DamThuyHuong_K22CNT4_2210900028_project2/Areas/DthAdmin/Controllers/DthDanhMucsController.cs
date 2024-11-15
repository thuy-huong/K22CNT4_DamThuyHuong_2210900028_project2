using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using PagedList;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Areas.DthAdmin.Controllers
{
    public class DthDanhMucsController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();

        // GET: DthAdmin/DthDanhMucs
        public ActionResult Index(int page =1)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");

            }
            var danhMucs = db.danhMucs.Include(d => d.danhMuc2);
            var pagedDanhMuc = danhMucs.OrderBy(d => d.maDanhMuc) 
                                .ToPagedList(page, 10);
            return View(pagedDanhMuc);
        }

        // GET: DthAdmin/DthDanhMucs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhMuc danhMuc = db.danhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // GET: DthAdmin/DthDanhMucs/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");
            }
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs
                    .Where(dm => dm.maDanhMucCha == null && dm.trangThai == true)
                    .ToList(),
                    "maDanhMuc",
                    "tenDanhMuc");
            return View();
        }

        // POST: DthAdmin/DthDanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDanhMuc,tenDanhMuc,maDanhMucCha,trangThai,ngayTao,ngayCapNhat")] danhMuc danhMuc)
        {
            var existingCate = db.danhMucs.FirstOrDefault(p => p.tenDanhMuc == danhMuc.tenDanhMuc);
            if (existingCate != null)
            {
                ModelState.AddModelError("tenDanhMuc", "Danhh muc đã tồn tại. Vui lòng nhập danh mục khác.");
            }
            if (ModelState.IsValid)
            {
                db.danhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maDanhMucCha = new SelectList(db.danhMucs.Where(dm => dm.maDanhMucCha == null && dm.trangThai == true)
                    .ToList(),
                    "maDanhMuc",
                    "tenDanhMuc");
            return View(danhMuc);
        }

        // GET: DthAdmin/DthDanhMucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhMuc danhMuc = db.danhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs.Where(dm => dm.maDanhMucCha == null && dm.trangThai == true)
                    .ToList(),
                    "maDanhMuc",
                    "tenDanhMuc");
            return View(danhMuc);
        }

        // POST: DthAdmin/DthDanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDanhMuc,tenDanhMuc,maDanhMucCha,trangThai,ngayTao,ngayCapNhat")] danhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs.Where(dm => dm.maDanhMucCha == null && dm.trangThai == true)
                    .ToList(),
                    "maDanhMuc",
                    "tenDanhMuc");
            return View(danhMuc);
        }

        // GET: DthAdmin/DthDanhMucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            danhMuc danhMuc = db.danhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: DthAdmin/DthDanhMucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            danhMuc danhMuc = db.danhMucs.Find(id);
            db.danhMucs.Remove(danhMuc);
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
