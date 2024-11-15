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
    public class DthHomeController : Controller
    {
        private K22CNT4_DamThuyHuong_2210900028_Project2Entities db = new K22CNT4_DamThuyHuong_2210900028_Project2Entities();
        public ActionResult DthIndex()
        {
            var data = new BannerSanPhamVM();
            data.sanPhams = db.sanPhams.Where(sp => sp.trangThai == true)
        
                         .OrderByDescending(p => p.ngayTao) 
                         .Take(10)
                         .ToList();
            data.banners = db.banners.Where(sp => sp.trangThai == true)
                             .OrderByDescending(p => p.maBanner)
                             .ToList();
            return View(data);
        }
        public PartialViewResult LayoutMenu()
        {
            var danhMucCha = new List<MenuCateViewModel>();
            var danhMucCon = new List<MenuCateConViewModel>();
            var danhMucChaConList = new List<danhMucChaConViewModel>();
            var data =  db.danhMucs.ToList();

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
           
            return PartialView("_LayoutMenu", danhMucChaConList);
        }
    
        public PartialViewResult MenuQuantityCart()
        {
            var gioHang = Session["CartSession"] as List<CartVM>; // Ép kiểu về List<Cart>
            var listCartItem = gioHang ?? new List<CartVM>(); 
            ViewBag.totalItem = listCartItem.Sum(p =>p.soLuong);
            return PartialView("_MenuQuantityCart");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}