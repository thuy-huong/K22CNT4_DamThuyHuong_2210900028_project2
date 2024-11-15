using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using DamThuyHuong_K22CNT4_2210900028_project2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Areas.DthAdmin.Controllers
{
    public class DthDashboardController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        // GET: DthAdmin/DthDashboard
        public ActionResult DthIndex()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin", "DthDashboard");
            }
            ViewBag.TongSanPham = db.khachHangs.Count();
            ViewBag.TongDonHang = db.donHangs.Count();
            ViewBag.TongKhachHang = db.khachHangs.Count();
            var data = new DashboardVM();
            data.donHangs = db.donHangs.ToList();

            data.KhachHang = db.khachHangs
                              .Select(kh => new
                              {
                                  KhachHang = kh,
                                  SoDonHang = db.donHangs.Count(dh => dh.maKhachHang == kh.maKhachHang)
                              })
                              .OrderByDescending(x => x.SoDonHang) 
                              .Take(8) 
                              .Select(x => x.KhachHang)
                              .ToList();
            var revenueData = db.donHangs
           .Where(d => d.ngayTao != null)
           .GroupBy(d => d.ngayTao.Value.Month)
           .Select(g => new RevenueDto
           {
               Month = g.Key,
               TotalRevenue = g.Sum(d => d.tongTien)
           }).ToList();

            ViewBag.RevenueData = revenueData;
            return View(data);
        }
        public ActionResult DthLogin(string ReturnUrl)
        {
            if (string.IsNullOrEmpty(ReturnUrl) && Request.UrlReferrer != null)
            {
                ReturnUrl = Request.UrlReferrer.ToString();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult DthLogin(string email, string matKhau, string ReturnUrl)
        {
            // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu không
            var user = db.quanTris.FirstOrDefault(u => u.email == email && u.matKhau == matKhau);

            if (user != null)
            {

                Session["user"] = user.tenQuanTri;

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("DthIndex", "DthDashboard");
                }
            }


            TempData["error"] = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        public ActionResult DthLogout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("DthLogin");
        }
        public ActionResult bieuDo()
        {
             var revenueData = db.donHangs
            .Where(d => d.ngayTao != null)
            .GroupBy(d => d.ngayTao.Value.Month)
            .Select(g => new RevenueDto
            {
                Month = g.Key,
                TotalRevenue = g.Sum(d => d.tongTien)
            }).ToList();

            ViewBag.RevenueData = revenueData;
            return View();
        }
    }
}