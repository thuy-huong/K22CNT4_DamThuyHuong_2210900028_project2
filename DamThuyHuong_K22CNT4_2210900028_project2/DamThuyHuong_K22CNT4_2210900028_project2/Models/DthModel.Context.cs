﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DamThuyHuong_K22CNT4_2210900028_project2.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class K22CNT4_DamThuyHuong_2210900028_Project2Entities : DbContext
    {
        public K22CNT4_DamThuyHuong_2210900028_Project2Entities()
            : base("name=K22CNT4_DamThuyHuong_2210900028_Project2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<banner> banners { get; set; }
        public virtual DbSet<chiTietDonHang> chiTietDonHangs { get; set; }
        public virtual DbSet<danhGia> danhGias { get; set; }
        public virtual DbSet<danhMuc> danhMucs { get; set; }
        public virtual DbSet<donHang> donHangs { get; set; }
        public virtual DbSet<khachHang> khachHangs { get; set; }
        public virtual DbSet<quanTri> quanTris { get; set; }
        public virtual DbSet<sanPham> sanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}