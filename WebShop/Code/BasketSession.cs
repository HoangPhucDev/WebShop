using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Code
{
    public class BasketSession
    {
        
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? Gia { get; set; }
        public int SoLuong { get; set; }

        public static void SetSession(BasketSession session)
        {
            HttpContext.Current.Session["BasketSession"] = session; 
        }

        public static BasketSession GetSession()
        {
            var session = HttpContext.Current.Session["BasketSession"];
            if (session == null)
                return null;
            else
            {
                return session as BasketSession;
            }
        }

        public static void RemoveSession()
        {
            HttpContext.Current.Session.Remove("BasketSession");
        }

        public static int GetTotal()
        {
           return HttpContext.Current.Session.Count;
        }
    }
}