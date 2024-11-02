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
    public class DthDanhMucsController : Controller
    {
        private DthEntities db = new DthEntities();

        // GET: DthAdmin/DthDanhMucs
        public ActionResult Index()
        {
            var danhMucs = db.danhMucs.Include(d => d.danhMuc2);
            return View(danhMucs.ToList());
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
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc");
            return View();
        }

        // POST: DthAdmin/DthDanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDanhMuc,tenDanhMuc,maDanhMucCha,trangThai,ngayTao,ngayCapNhat")] danhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                db.danhMucs.Add(danhMuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maDanhMucCha = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", danhMuc.maDanhMucCha);
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
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", danhMuc.maDanhMucCha);
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
            ViewBag.maDanhMucCha = new SelectList(db.danhMucs, "maDanhMuc", "tenDanhMuc", danhMuc.maDanhMucCha);
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
