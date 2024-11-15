using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using DamThuyHuong_K22CNT4_2210900028_project2.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Controllers
{
    public class DthSanPhamController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        // GET: DthSanPham
        public ActionResult DthIndex(int? cate, int page=1, string sortOrder = "default")
        {

            List<sanPham> danhsachSanPham = new List<sanPham>();
            if (cate.HasValue)
            {
                danhsachSanPham = db.sanPhams.Where(sp => sp.trangThai == true && cate.Value == sp.maDanhMuc)
                        .OrderByDescending(p => p.ngayTao)
                                               .ToList(); 
            }
            else
            {
                danhsachSanPham = db.sanPhams.Where(sp => sp.trangThai == true)
                      .OrderByDescending(p => p.ngayTao)
                                             .ToList();

            }
            switch (sortOrder)
            {
                case "priceLow":
                    danhsachSanPham = danhsachSanPham.OrderBy(sp => sp.gia).ToList();
                    break;
                case "priceHigh":
                    danhsachSanPham = danhsachSanPham.OrderByDescending(sp => sp.gia).ToList();
                    break;
                case "nameDesc":
                    danhsachSanPham = danhsachSanPham.OrderByDescending(sp => sp.tenSanPham).ToList();
                    break;
                default:
                    break;
            }
            var pagedProducts = danhsachSanPham.ToPagedList(page, 6);
            ViewBag.cate = cate;
            return View(pagedProducts);
        }
        
        public ActionResult DthSearch(string query, int page = 1, string sortOrder = "default")
        {
            List<sanPham> danhsachSanPham = new List<sanPham>();
            if (!string.IsNullOrEmpty(query))
            {
                danhsachSanPham = db.sanPhams
                    .Where(sp => sp.trangThai == true && sp.tenSanPham.Contains(query)) 
                    .OrderByDescending(p => p.ngayTao)
                    .ToList();
            }
            switch (sortOrder)
            {
                case "priceLow":
                    danhsachSanPham = danhsachSanPham.OrderBy(sp => sp.gia).ToList();
                    break;
                case "priceHigh":
                    danhsachSanPham = danhsachSanPham.OrderByDescending(sp => sp.gia).ToList();
                    break;
                case "nameDesc":
                    danhsachSanPham = danhsachSanPham.OrderByDescending(sp => sp.tenSanPham).ToList();
                    break;
                default:
                    break;
            }
            var pagedProducts = danhsachSanPham.ToPagedList(page, 6);
            ViewBag.query = query;
            return View(pagedProducts);
        }
        public ActionResult DthDetail(int id)
        {
            var productDetail = new DetailsProductVM();
            var  sanPham = db.sanPhams.Find(id);


            if (sanPham == null)
            {
                return HttpNotFound(); 
            }
            productDetail.sanPham = sanPham;
            productDetail.danhGias = db.danhGias.Where(p =>p.maSanPham == sanPham.maSanPham).ToList();
            return View(productDetail);
        }
        [HttpPost]
        public ActionResult DthAddDanhGia(int maSanPham, int xepHang, string binhLuan)
        {
            if (Session["cusId"] == null)
            {
                return RedirectToAction("DthLogin", "DthKhachHang");
            }
            else
            {
                var cusId = Convert.ToInt32(Session["cusId"]); ;
                var danhGia = new danhGia
                {
                    maKhachHang = cusId,
                    maSanPham = maSanPham,
                    xepHang = xepHang,
                    binhLuan = binhLuan
                };
                db.danhGias.Add(danhGia);
                db.SaveChanges();
            }
            return RedirectToAction("DthDetail", new { id = maSanPham });
        }
        public PartialViewResult DanhMucMenu()
        {
            var danhMucCha = new List<MenuCateViewModel>();
            var danhMucCon = new List<MenuCateConViewModel>();
            var danhMucChaConList = new List<danhMucChaConViewModel>();
        
            // Lấy dữ liệu từ cơ sở dữ liệu
            var data = db.danhMucs.ToList();

            // Phân loại danh mục cha và con
            foreach (var item in data)
            {
                if (item.maDanhMucCha != null)
                {
                    var dmCon = new MenuCateConViewModel
                    {
                        MaDanhMuc = item.maDanhMuc,
                        TenDanhMuc = item.tenDanhMuc,
                        MaDanhMucCha = item.maDanhMucCha
                    };
                    danhMucCon.Add(dmCon);
                }
                else
                {
                    var dmCha = new MenuCateViewModel
                    {
                        MaDanhMuc = item.maDanhMuc,
                        TenDanhMuc = item.tenDanhMuc
                    };
                    danhMucCha.Add(dmCha);
                }
            }

            // Tạo danh sách danh mục cha và danh mục con
            foreach (var item in danhMucCha)
            {
                var dmChaCon = new danhMucChaConViewModel
                {
                    DanhChaMuc = item,
                    listDanhMucCon = new List<MenuCateConViewModel>() // Khởi tạo danh sách
                };

                foreach (var item2 in danhMucCon)
                {
                    if (item2.MaDanhMucCha == item.MaDanhMuc)
                    {
                        dmChaCon.listDanhMucCon.Add(item2);
                    }
                }

                danhMucChaConList.Add(dmChaCon);
            }
            return PartialView("_DanhMucMenu", danhMucChaConList);
        }
      
    }
}