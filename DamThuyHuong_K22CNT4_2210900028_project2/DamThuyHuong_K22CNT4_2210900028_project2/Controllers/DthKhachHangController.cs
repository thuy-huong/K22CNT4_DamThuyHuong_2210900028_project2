using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using DamThuyHuong_K22CNT4_2210900028_project2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Controllers
{
    public class DthKhachHangController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        // GET: DthKhachHang
        public ActionResult DthLogin(string ReturnUrl)
        {
            // Nếu ReturnUrl không được truyền, lấy từ URL trước đó
            if (string.IsNullOrEmpty(ReturnUrl) && Request.UrlReferrer != null)
            {
                ReturnUrl = Request.UrlReferrer.ToString();
            }

            ViewBag.ReturnUrl = ReturnUrl; // Lưu trữ ReturnUrl vào ViewBag
            return View();
        }
        [HttpPost]
        public ActionResult DthLogin(string email, string matKhau, string ReturnUrl)
        {

            var cus = db.khachHangs.FirstOrDefault(u => u.email == email && u.matKhau == matKhau);

            if (cus != null)
            {
                Session["cusName"] = cus.tenKhachHang;
                Session["cusId"] = cus.maKhachHang;

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("DthIndex", "DthHome");
                }
            }

            TempData["error"] = "Tài khoản hoặc mật khẩu không đúng!";
        
            return View();

        }
        public ActionResult DthLogout()
        {
            Session.Remove("cusName");
            Session.Remove("cusId"); 
            //FormsAuthentication.SignOut();
            return RedirectToAction("DthIndex", "DthHome");
        }

        public ActionResult DthSignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DthSignIn( string tenKhachHang, string dienThoai, string diaChi, string email, string matKhau, HttpPostedFileBase file)
        {
            var existingCus = db.khachHangs.FirstOrDefault(p => p.email == email);
            if (existingCus != null)
            {
                TempData["error"] =  "Tài khoản đã tồn tại. Vui lòng nhập email khác.";
                return View();
            }
            else
            {
                var cus = new khachHang
                {
                    tenKhachHang = tenKhachHang,
                    dienThoai = dienThoai,
                    diaChi = diaChi,
                    email = email,
                    matKhau = matKhau,
                };


                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Public/UpLoadImg/imgCus"), fileName);
                    file.SaveAs(path);

                    cus.hinhAnh = fileName;
                }
                else
                {
                    cus.hinhAnh = "DEFAULT_USER.jpg";
                }

                db.khachHangs.Add(cus);
                db.SaveChanges();
                return View("DthLogin");
            }
        }
        public ActionResult DthProfile(int id) 
        {
            ProfileCusVM profile = new ProfileCusVM();
            profile.khachHang = db.khachHangs.Find(id);
            profile.donHangs = db.donHangs.Where(p => p.maKhachHang == id).ToList();
            return View(profile); 
        }
        [HttpPost]
        public ActionResult DthUpdate(HttpPostedFileBase file,int maKhachHang ,string tenKhachHang, string email, string diaChi, string dienThoai)
        {
            var khachHang = db.khachHangs.Find(maKhachHang);
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Public/UpLoadImg/imgCus"), fileName);
                file.SaveAs(path);

                khachHang.hinhAnh = fileName;
            }
            khachHang.tenKhachHang=tenKhachHang;
            khachHang.email = email;
            khachHang.diaChi = diaChi;
            khachHang.dienThoai= dienThoai;
            db.Entry(khachHang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthProfile", new { id = maKhachHang });
        }
        [HttpPost]
        public ActionResult DthHuyDonHang(int maDonHang, int maKhachHang)
        {
            var dh = db.donHangs.Find(maDonHang);
            dh.trangThai = 4;
            dh.ngayCapNhat = DateTime.Now;
            db.Entry(dh).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DthProfile", new { id = maKhachHang });
        }
        public ActionResult DthChangePassword()
        {
            ViewBag.id = Session["cusId"];
            return View();
        }
        [HttpPost]
        public ActionResult DthChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }     
            var user = db.khachHangs.Find(model.IdCus); 

            if (user == null)
            {
                ModelState.AddModelError("", "Người dùng không tồn tại.");
                return View(model);
            }

            if (user.matKhau != (model.CurrentPassword))
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng.");
                return View(model);
            }


            if (model.NewPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp.");
                return View(model);
            }

            // Cập nhật mật khẩu
            user.matKhau = model.NewPassword;
            db.SaveChanges();

            return RedirectToAction("DthProfile", new { id = model.IdCus }); 
        }
    }
}