using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web;


namespace WebShop.Code
{
    public class SessionHelper 
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["LoginSession"] = session;
            if(session.RememberMe)
            {
                HttpContext.Current.Session.Timeout = 1440;
            }
        }

        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["LoginSession"];
            if(session == null)
                return null;
           else
            {
                return session as UserSession;
            }
        }

        public static void RemoveSession()
        {
            HttpContext.Current.Session.Remove("LoginSession");
        }

    }
}
