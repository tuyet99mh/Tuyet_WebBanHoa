using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models
{
    public class RegisterModel
    {
        public string email { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
    }
}