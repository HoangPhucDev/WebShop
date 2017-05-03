using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebShop.Code
{
    public static class FillterString
    {
        //private static readonly string[] VietNamChar = new string[]
        //{
        //    "aAeEoOuUiIdDyY",
        //    "áàạảãâấầậẩẫăắằặẳẵ",
        //    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        //    "éèẹẻẽêếềệểễ",
        //    "ÉÈẸẺẼÊẾỀỆỂỄ",
        //    "óòọỏõôốồộổỗơớờợởỡ",
        //    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        //    "úùụủũưứừựửữ",
        //    "ÚÙỤỦŨƯỨỪỰỬỮ",
        //    "íìịỉĩ",
        //    "ÍÌỊỈĨ",
        //    "đ",
        //    "Đ",
        //    "ýỳỵỷỹ",
        //    "ÝỲỴỶỸ"
        //};
        //public static string LocDau(this string str)
        //{
        //    //Thay thế và lọc dấu từng char      
        //    for (int i = 1; i < VietNamChar.Length; i++)
        //    {
        //        for (int j = 0; j < VietNamChar[i].Length; j++)
        //            str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
        //    }
        //    return str;
        //}
        public static string ConvertToUnSign(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            String temp = "".Normalize(NormalizationForm.FormD);
            if (s !=null)
            {
                temp = s.Normalize(NormalizationForm.FormD);
            }
            
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}