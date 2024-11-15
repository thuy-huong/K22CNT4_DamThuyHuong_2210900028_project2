using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Areas.DthAdmin.Controllers
{
    public class DthKhachHangsController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();

        // GET: DthAdmin/DthKhachHang
        public ActionResult DthIndex(int page =1)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");
            }
            var khachHangs = db.khachHangs;

            var pagedkhachHangs = khachHangs.OrderBy(d => d.maKhachHang).ToPagedList(page, 5);
            return View(pagedkhachHangs);
        }
        public ActionResult DthKhoa(int maKhachHang)
        {
            var kh = db.khachHangs.Find(maKhachHang);
            kh.trangThai = false;
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthMoKhoa(int maKhachHang)
        {
            var kh = db.khachHangs.Find(maKhachHang);
            kh.trangThai = true;
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthDetail(int id)
        {
            var kh = db.khachHangs.Find(id);
            return View(kh);
        }
    }
}