﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.Code
{
    public class BasketSession
    {

        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal? Gia { get; set; }
        public int SoLuong { get; set; }

        public static void AddSession(BasketModel ItemBasket)
        {
            var basket = HttpContext.Current.Session["BasketSession"];
            if (basket != null)
            {
                var list = (List<BasketModel>)basket;

                if (list.Exists(p => p.MaSP == ItemBasket.MaSP))
                {
                    foreach (var item in list)
                    {
                        if (item.MaSP == ItemBasket.MaSP)
                        {
                            item.SoLuong += ItemBasket.SoLuong;
                        }
                    }
                }
                else
                {
                    list.Add(ItemBasket);
                    HttpContext.Current.Session["BasketSession"] = list;
                }
            }
            else
            {
                var list = new List<BasketModel>();
                list.Add(ItemBasket);
                HttpContext.Current.Session["BasketSession"] = list;
            }
        }
        public static bool RemoveItem(int IdItem)
        {

            var basket = HttpContext.Current.Session["BasketSession"];
            if (basket != null)
            {
                var list = (List<BasketModel>)basket;
                
                if (list.Exists(p => p.MaSP == IdItem))
                {  
                    var rs = list.Remove(list.Where(p => p.MaSP == IdItem).SingleOrDefault());
                    if (list.Count == 0)
                    {
                        RemoveSession();
                    }
                    return rs;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool GiamItem(int IdItem)
        {
            var basket = HttpContext.Current.Session["BasketSession"];
            if (basket != null)
            {
                var list = (List<BasketModel>)basket;
                if (list.Exists(p => p.MaSP == IdItem))
                {
                    foreach (var item in list)
                    {
                        if (item.MaSP == IdItem)
                        {
                            item.SoLuong--;
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static List<BasketModel> GetSession()
        {
            var session = HttpContext.Current.Session["BasketSession"];
            if (session == null)
                return null;
            else
            {
                return session as List<BasketModel>;
            }
        }

        public static void RemoveSession()
        {
            HttpContext.Current.Session.Remove("BasketSession");
        }


    }
}