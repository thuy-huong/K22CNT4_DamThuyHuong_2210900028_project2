using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using DamThuyHuong_K22CNT4_2210900028_project2.ViewModels;
using Microsoft.Ajax.Utilities;
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
    public class DthDonHangController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        // GET: DthAdmin/DthDonHang
        public ActionResult DthIndex( int page = 1)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");
            }
            var donHangs = db.donHangs.ToList();

            var pagedDonHang = donHangs.ToPagedList(page, 5);
            return View(pagedDonHang);
        }
        [HttpPost]
        public ActionResult DthXuLy(int maDonHang)
        {
            var dh = db.donHangs.Find(maDonHang);
            dh.trangThai = 2;
            dh.ngayCapNhat = DateTime.Now;
            db.Entry(dh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthGiaHangThanhCong(int maDonHang)
        {
            var dh = db.donHangs.Find(maDonHang);
            dh.trangThai = 3;
            dh.ngayCapNhat = DateTime.Now;
            db.Entry(dh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthDetail(int id)
        {
            ChiTietDonHangVM cthd = new ChiTietDonHangVM();
            cthd.donHang = db.donHangs.Find(id);
            cthd.listSanPham = db.chiTietDonHangs.Where(p => p.maDonHang == id).ToList();
            return View(cthd);
        }
    }
}