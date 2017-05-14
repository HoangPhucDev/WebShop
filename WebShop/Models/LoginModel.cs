using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{

    public class LoginModel
    {
            [Required(ErrorMessage = "Không Để Trống")]
            public string Username { get; set; }
            [Required(ErrorMessage = "Không Để Trống")]
            public string Password { get; set; }
            public bool RememberMe { get; set; }
    }
}