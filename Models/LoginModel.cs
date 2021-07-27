using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Anemone.Models
{
    public class LoginModel
    {
        public string email { get; set; }  
        
        public string password { get; set; }

        public string name { get; set; }
    }
}