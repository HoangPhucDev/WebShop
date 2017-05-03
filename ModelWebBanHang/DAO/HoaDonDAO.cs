using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
  public  class HoaDonDAO
    {
        static HoaDonDAO instance;
        static object key = new object();
        internal static HoaDonDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new HoaDonDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public HoaDonDAO()
        {
            db = new WebBanHang();
        }
    }
}
