using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
 public   class NhanVienDAO
    {
        static NhanVienDAO instance;
        static object key = new object();
        public static NhanVienDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new NhanVienDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public NhanVienDAO()
        {
            db = new WebBanHang();
        }

        public bool CheckLogin(string user, string password)
        {
            bool rs = false;

            int query = db.NHAN_VIEN.Count(p => user == p.MA_NV && password == p.MAT_KHAU);

                if (query == 1)
                {
                    rs = true;
                }
                return rs;

        }
    }
}
