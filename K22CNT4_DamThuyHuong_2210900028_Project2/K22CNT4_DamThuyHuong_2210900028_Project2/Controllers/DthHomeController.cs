using K22CNT4_DamThuyHuong_2210900028_Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K22CNT4_DamThuyHuong_2210900028_Project2.Controllers
{
    public class DthHomeController : Controller
    {
        private DthEntities db = new DthEntities();
        public ActionResult DthIndex()
        {
            var products = db.sanPhams
                     .OrderByDescending(p => p.ngayTao) // Sắp xếp theo ngày tạo giảm dần
                     .Take(10) // Lấy 10 sản phẩm
                     .ToList(); // Chuyển đổi thành danh sách

            return View(products);
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