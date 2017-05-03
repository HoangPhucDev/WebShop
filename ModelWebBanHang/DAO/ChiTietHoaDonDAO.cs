using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
   public class ChiTietHoaDonDAO
    {
        static ChiTietHoaDonDAO instance;
        static object key = new object();
        internal static ChiTietHoaDonDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new ChiTietHoaDonDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public ChiTietHoaDonDAO()
        {
            db = new WebBanHang();
        }
    }
}
