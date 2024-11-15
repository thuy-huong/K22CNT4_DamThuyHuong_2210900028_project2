using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using DamThuyHuong_K22CNT4_2210900028_project2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Controllers
{
    public class DthCartController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        private const string CartSession = "CartSession";
        // GET: DthCart
        public ActionResult DthIndex()
        {
            var gioHang = Session[CartSession] as List<CartVM>; // Ép kiểu về List<Cart>
            var listCartItem = gioHang ?? new List<CartVM>(); // Nếu null, khởi tạo danh sách mới
            return View(listCartItem);
        }

        [HttpPost]
        public ActionResult DthAddItem(int maSanPham, int soLuong)
        {
            var sanPham = db.sanPhams.FirstOrDefault(p => p.maSanPham == maSanPham);

            if (sanPham == null)
            {
                return HttpNotFound();
            }

            var gioHang = Session[CartSession] as List<CartVM> ?? new List<CartVM>();

            var existingItem = gioHang.FirstOrDefault(x => x.sanPham.maSanPham == maSanPham);
            if (existingItem != null)
            {
                existingItem.soLuong += soLuong;
            }
            else
            {
                gioHang.Add(new CartVM
                {
                    sanPham = sanPham,
                    soLuong = soLuong
                });
            }

            Session[CartSession] = gioHang;

            return RedirectToAction("DthIndex");
        }
        public ActionResult DeleteAllCart()
        {
            Session[CartSession] = null;
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthRemoveItemCart(int id)
        {
            var gioHang = Session[CartSession] as List<CartVM>;
            var item = gioHang.SingleOrDefault(p => p.sanPham.maSanPham == id);
            if (item != null)
            {
                gioHang.Remove(item);
                Session[CartSession] = gioHang;
            }
            return RedirectToAction("DthIndex");
        }
        public ActionResult DthThanhToan()
        {
            if (Session["cusId"] == null)
            {
                return RedirectToAction("DthLogin", "DthKhachHang");
            }
            var gioHang = Session[CartSession] as List<CartVM>;
            if (gioHang == null )
            {
                return RedirectToAction("DthIndex", "DthSanPham");
            }
              
            return View(gioHang);
        }
        [HttpPost]
        public ActionResult DthThanhToan(bool GiongKhachHang, string tenKhachHang, string diaChi, string dienThoai, int phuongThucThanhToan)
        {
            var gioHang = Session[CartSession] as List<CartVM>;
            var cusId = Session["cusId"];

            // Kiểm tra giỏ hàng
            if (gioHang == null || gioHang.Count == 0)
            {
                return RedirectToAction("DthIndex", "DthHome"); 
            }

            var donHang = new donHang();
            var Customer = db.khachHangs.Find(cusId);

            // Thiết lập thông tin cho đơn hàng
            if (GiongKhachHang)
            {
                donHang.tenKhachHang = Customer.tenKhachHang;
                donHang.dienthoai = Customer.dienThoai;
                donHang.diaChiGiaoHang = Customer.diaChi;
            }
            else
            {
                donHang.tenKhachHang = tenKhachHang;
                donHang.dienthoai = dienThoai;
                donHang.diaChiGiaoHang = diaChi;
            }
            donHang.trangThai = 1;
            donHang.maKhachHang = Customer.maKhachHang;
            donHang.phuongThucThanhToan = phuongThucThanhToan;
            donHang.tongTien = gioHang.Sum(s => s.ThanhTien);
            donHang.phiVanChuyen = 0;
            donHang.ngayTao = DateTime.Now;
            donHang.ngayCapNhat = DateTime.Now;

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Lưu đơn hàng
                    db.donHangs.Add(donHang);
                    db.SaveChanges();

                    var cthd = new List<chiTietDonHang>();

                    foreach (var item in gioHang)
                    {
                        // Lưu chi tiết đơn hàng
                        cthd.Add(new chiTietDonHang
                        {
                            maDonHang = donHang.maDonHang, 
                            maSanPham = item.sanPham.maSanPham,
                            soLuong = item.soLuong,
                            gia = item.sanPham.gia,
                        });

                        // Cập nhật số lượng sản phẩm
                        var sanPham = db.sanPhams.Find(item.sanPham.maSanPham);
                        if (sanPham != null)
                        {
                            sanPham.soLuong -= item.soLuong; 
                            db.Entry(sanPham).State = EntityState.Modified;
                        }
                    }

                    // Lưu chi tiết đơn hàng
                    db.chiTietDonHangs.AddRange(cthd);
                    db.SaveChanges();

                    // Xóa giỏ hàng
                    Session[CartSession] = null; // Hoặc xóa theo cách bạn đã định nghĩa

                    // Cam kết giao dịch
                    transaction.Commit();

                    return View("DthSuccess");
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi, rollback giao dịch
                    transaction.Rollback();
                    
                }
            }

            return View("Error"); 
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int maSanPham, int soLuong)
        {
            var gioHang = Session["CartSession"] as List<CartVM>;
            if (gioHang != null)
            {
                var item = gioHang.FirstOrDefault(x => x.sanPham.maSanPham == maSanPham);
                if (item != null)
                {
                    item.soLuong = soLuong; // Cập nhật số lượng
                    if (item.soLuong <= 0)
                    {
                        gioHang.Remove(item); // Nếu số lượng <= 0, xóa sản phẩm khỏi giỏ hàng
                    }
                }

                // Cập nhật lại giỏ hàng vào session
                Session["CartSession"] = gioHang;
            }

            return Json(new { success = true });
        }
    }
}