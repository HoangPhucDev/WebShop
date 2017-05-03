using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
   public class KhachHangDAO
    {
        static KhachHangDAO instance;
        static object key = new object();
        internal static KhachHangDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new KhachHangDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public KhachHangDAO()
        {
            db = new WebBanHang();
        }
    }
}
