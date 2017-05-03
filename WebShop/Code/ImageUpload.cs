using ModelWebBanHang;
using ModelWebBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WebShop.Code
{
    public class ImageUpload 
    {
       public string FileName { set; get; }
       public string ThongBao { set; get; }
       public string PathServer { set; get; }
       public string PathFile {set;get;}

       //Lấy Thư mục chứa ảnh trên sever khi tạo đối tượng
        public ImageUpload(string pathserver)
        {
            PathServer = pathserver;
        }
        public bool KiemTraThemAnhMoi(HttpPostedFileBase fileHinhAnh)
        {
           
            var filename = Path.GetFileName(fileHinhAnh.FileName);
            var path = Path.Combine(PathServer, filename);
            if (System.IO.File.Exists(path))
                {
                ThongBao = "File Ảnh Đã Tồn Tại";
                return false;
            }
                else
                {
                GetNewPathForDupes(path, ref filename);
                return true;
            }
        }

        public bool KiemTraSuaAnh(HttpPostedFileBase fileHinhAnh,string AnhGoc)
        {

            if (fileHinhAnh != null)
            {
                return KiemTraThemAnhMoi(fileHinhAnh);
            }
            else
            {
                FileName = AnhGoc;
                return true;
            }

        }
        #region xử lý ảnh
        public bool SaveResizeImage(Image img, string Newpath)
        {
            try
            {
                //// lấy chiều rộng và chiều cao ban đầu của ảnh
                //int originalW = img.Width;
                //int originalH = img.Height;
                //// lấy chiều rộng và chiều cao mới tương ứng với chiều rộng truyền vào của ảnh (nó sẽ giúp ảnh của chúng ta sau khi resize vần giứ được độ cân đối của tấm ảnh
                //int resizedW = width;
                //int resizedH = (originalH * resizedW) / originalW;
                int resizedW = 500;
                int resizedH = 500;

                Bitmap b = new Bitmap(resizedW, resizedH);
                Graphics g = Graphics.FromImage(b);

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.High;    // Specify here
                Rectangle rectDestination = new Rectangle(0, 0, resizedW, resizedH);
                g.DrawImage(img, rectDestination, 0, 0, img.Width, img.Height,GraphicsUnit.Pixel);
              
                b.Save(Newpath,System.Drawing.Imaging.ImageFormat.Jpeg);
                g.Dispose();
                b.Dispose();
                img.Dispose();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion
        //hàm lấy đường dẫn mới khki file đã tồn tại
        public string GetNewPathForDupes(string path, ref string fn)
        {
            string directory = Path.GetDirectoryName(path);
            string filename = Path.GetFileNameWithoutExtension(path);
            string extension = Path.GetExtension(path);
            int counter = 1;
            string newFullPath = path;
            string new_file_name = filename + ".jpg";
            while (System.IO.File.Exists(newFullPath))
            {
                string newFilename = string.Format("{0}({1}){2}", filename, counter, extension);
                new_file_name = newFilename;
                newFullPath = Path.Combine(directory, newFilename);
                counter++;
            };
            fn = new_file_name;
            FileName = new_file_name;
            PathFile = newFullPath;
            return newFullPath;
        }
    }
}