using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPshop.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="You must input user name.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "You must input password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}