using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Code
{
    public static class MyLinq
    {
        //Extension method
        public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource,bool> func)
        {
            //Loop toàn bộ cái item trong danh sách truyền vào
            foreach (var item in source)
            {
                // Callback lại hàm đã truyền vào, hàm này trả giá trị boolean
                // Nếu hàm callback return true, đưa item đó vào IENumerable kết quả
                // Xem lại vài IEnumerable và yield
                if (func(item)) yield return item;
            }
        }
    }
}