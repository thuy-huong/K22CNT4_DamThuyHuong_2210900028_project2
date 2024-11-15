using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamThuyHuong_K22CNT4_2210900028_project2.ViewModels
{
    public class MenuCateViewModel
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }

    }
    public class MenuCateConViewModel
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public int? MaDanhMucCha { get; set; }
    }
    public class danhMucChaConViewModel
    {
        public MenuCateViewModel DanhChaMuc { get; set; }
        public List<MenuCateConViewModel> listDanhMucCon { get; set; }
    }
}