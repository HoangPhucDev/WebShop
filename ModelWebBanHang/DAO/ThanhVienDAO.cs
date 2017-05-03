using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
  public  class ThanhVienDAO
    {
        static ThanhVienDAO instance;
        static object key = new object();
        public static ThanhVienDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new ThanhVienDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public ThanhVienDAO()
        {
            db = new WebBanHang();
        }

        public List<THANH_VIEN> GetListMember()
        {
            List<THANH_VIEN> list = db.THANH_VIEN.Select(p => p).ToList();
            return list; 
        }

        public bool CheckLogin(string user, string password)
        {
            bool rs = false;

            int query = db.THANH_VIEN.Count(p => user == p.MA_TV && password == p.MAT_KHAU);

            if (query == 1)
            {
                rs = true;
            }
            return rs;

        }
    }
}
