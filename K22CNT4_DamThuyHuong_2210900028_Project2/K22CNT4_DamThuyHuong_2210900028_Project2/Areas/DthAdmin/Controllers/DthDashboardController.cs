using K22CNT4_DamThuyHuong_2210900028_Project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace K22CNT4_DamThuyHuong_2210900028_Project2.Areas.DthAdmin.Controllers
{
    public class DthDashboardController : Controller
    {
        private DthEntities db = new DthEntities();
        // GET: DthAdmin/DthDashboard
        public ActionResult DthIndex()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DthLogin");
            }
            return View();
        }
        public ActionResult DthLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DthLogin(String email, String matKhau)
        {
            // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu không
            var user = db.quanTris.FirstOrDefault(u => u.email == email && u.matKhau == matKhau);

            if (user != null)
            {

                Session["user"] = user.tenQuanTri;
                return RedirectToAction("DthIndex");
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
    }
}