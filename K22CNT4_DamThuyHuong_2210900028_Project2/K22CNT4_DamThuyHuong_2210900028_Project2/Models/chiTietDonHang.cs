//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace K22CNT4_DamThuyHuong_2210900028_Project2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class chiTietDonHang
    {
        public int maChiTiet { get; set; }
        public Nullable<int> maDonHang { get; set; }
        public Nullable<int> maSanPham { get; set; }
        public int soLuong { get; set; }
        public decimal gia { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public Nullable<System.DateTime> ngayCapNhat { get; set; }
    
        public virtual donHang donHang { get; set; }
        public virtual sanPham sanPham { get; set; }
    }
}