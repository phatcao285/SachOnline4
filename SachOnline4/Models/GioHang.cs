using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SachOnline4.Models
{
    public class GioHang
    {

        SachOnlineEntities db = new SachOnlineEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public int dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
          get  { return iSoLuong * dDonGia; }
        }

        public GioHang(int ms)
        {
            iMaSach = ms;
            SACH s = db.SACHes.Single(n => n.MaSach == iMaSach);
            sTenSach = s.TenSach;
            sAnhBia = s.AnhBia;
            dDonGia = (int)double.Parse(s.GiaBan.ToString());
            iSoLuong = 1;
        }


    }
}