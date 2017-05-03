using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
   public class NhaCungCapDAO
    {
        static NhaCungCapDAO instance;
        static object key = new object();
        public static NhaCungCapDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new NhaCungCapDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public NhaCungCapDAO()
        {
            db = new WebBanHang();
        }

        public List<NHA_CUNG_CAP> GetListSupplier()
        {
            var List = db.SP_NHA_CUNG_CAP_SELECT_ALL().ToList();

            return List;
        }
    }
}
