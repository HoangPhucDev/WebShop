using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelWebBanHang.DAO
{
   public class SanPhamDAO
    {

        static SanPhamDAO instance;
        static object key = new object();
        public static SanPhamDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        instance = new SanPhamDAO();
                    }
                }
                return instance;
            }
        }

        private WebBanHang db = null;
        public SanPhamDAO()
        {
            db = new WebBanHang();
        }

        public List<SAN_PHAM> GetListProductAll()
        {
                List<SAN_PHAM> list = db.SP_SAN_PHAM_SELECT_ALL().ToList();
    
                return list;
        }

        public List<SAN_PHAM> GetListProductById(int id)
        {
                List<SAN_PHAM> list = db.SP_SAN_PHAM_SELECT_By_MaSp(id).ToList();

                return list;
        }

        public List<SAN_PHAM> GetListProductByGroup(int idGroup)
        {
                List<SAN_PHAM> list = db.SP_SAN_PHAM_SELECT_By_MaNhomSp(idGroup).ToList();
                return list;
        }

        public List<SAN_PHAM> GetListProductByGroupAndSpecies(int idGroup)
        {
                List<SAN_PHAM> list = db.SP_SAN_PHAM_SELECT_By_MaNhomSp_BaoGom_MaNhomCha(idGroup).ToList();
                return list;
        }

        public List<SAN_PHAM> Find(String Name)
        {
            List<SAN_PHAM> list = db.SP_SAN_PHAM_FIND(true,0,Name,"","",0,0,0,"",new DateTime(),0,1,0,0,0).ToList();

            return list;
        }
        public int InsertProduct(SAN_PHAM sp)
        {
            sp.NGAY_NHAP = DateTime.Today; 
            db.SAN_PHAM.Add(sp);
            db.SaveChanges();
            return sp.MA_SP;
        }
        public int DeletetProduct(int id)
        {
          int resul= db.SP_SAN_PHAM_DELETE(id);
            return resul;
        }

        public int UpdateProduct(SAN_PHAM sp)
        {
            
            int resul = db.SP_SAN_PHAM_UPDATE(sp.MA_SP,
                                                sp.TEN_SP,
                                                sp.THONG_SO_KY_THUAT,
                                                sp.CHITIET,
                                                sp.GIA_NHAP,
                                                sp.GIA_BAN,
                                                sp.SO_LUONG,
                                                sp.HINH_ANH,
                                                sp.CAN_NANG,
                                                sp.TRANG_THAI,
                                                sp.THOI_GIAN_BAO_HANH,
                                                sp.MA_NHOM_SP,
                                                sp.MA_NCC,
                                                sp.NGAY_SUA,
                                                sp.NGUOI_SUA);
            return resul;
        }
    }

}

