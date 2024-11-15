using DamThuyHuong_K22CNT4_2210900028_project2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamThuyHuong_K22CNT4_2210900028_project2.ViewModels
{
    public class ChiTietDonHangVM
    {
        public donHang donHang { get; set; }
        public List<chiTietDonHang> listSanPham { get; set; }
    }
}