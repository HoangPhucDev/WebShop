using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
  public  class NhomSanPhamDAO
    {
        static NhomSanPhamDAO instance;
        static object key = new object();
        private WebBanHang db = null;
        public static NhomSanPhamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new NhomSanPhamDAO();
                    }
                }
                return instance;
            }
        }

        public NhomSanPhamDAO()
        {
            db = new WebBanHang();
        }

        public List<NHOM_SAN_PHAM> GetCatalogueAll()
        {
            var list = db.SP_NHOM_SAN_PHAM_SELECT_ALL().ToList();
            return list;
        }
        public List<NHOM_SAN_PHAM> GetCatalogue()
        {

                List<NHOM_SAN_PHAM> List = (from p in db.NHOM_SAN_PHAM
                                            where p.MA_NHOM_CHA == 0
                                            select p).ToList();
                return List;
        }

        public List<NHOM_SAN_PHAM> GetSubCatalogue(int IdParent)
        {

                List<NHOM_SAN_PHAM> List = (from p in db.NHOM_SAN_PHAM
                                            where p.MA_NHOM_CHA == IdParent
                                            select p).ToList();
                return List;
        }
    }
}
