using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Areas.DthAdmin.Controllers
{
    public class DthBannerController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        // GET: DthAdmin/DthBanner
        public ActionResult DthIndex()
        {
            //if (Session["user"] == null)
            //{
            //    return RedirectToAction("DthLogin", "DthDashboard");
            //}
            var banner = db.banners.ToList();
            return View(banner);
        }
        [HttpPost]
        public ActionResult DthAddBanner(HttpPostedFileBase file, bool trangThai)
        {
            var banner = new banner();
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Public/UpLoadImg/imgBanner"), fileName);
                file.SaveAs(path);
                banner.hinhAnh = fileName;
            }
            banner.trangThai = trangThai;
            db.banners.Add(banner);
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        [HttpPost]
        public ActionResult DthDelete(int maBanner)
        {
            var banner = db.banners.Find(maBanner);
            db.banners.Remove(banner);
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        [HttpPost]
        public ActionResult DthHienThi(int maBanner)
        {
            var banner = db.banners.Find(maBanner);
            banner.trangThai = true;
            db.Entry(banner).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        [HttpPost]
        public ActionResult DthAn(int maBanner)
        {
            var banner = db.banners.Find(maBanner);
            banner.trangThai = false;
            db.Entry(banner).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthDetail(int id)
        {
           
            return View();
        }
    }
}