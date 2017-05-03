using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Code
{
    [Serializable]
    public class UserSession
    {
        public string UserName { set; get; }
        
        public bool RememberMe { set; get; }



        }
    }
