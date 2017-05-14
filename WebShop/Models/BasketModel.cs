using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class BasketModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? Gia { get; set; }
        public int SoLuong { get; set; }
    }
}