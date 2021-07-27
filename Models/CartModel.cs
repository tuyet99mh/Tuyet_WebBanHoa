using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models
{
    public class CartModel
    {
        public int idp { get; set; }
        public string codepr { get; set; }
        public string name { get; set; }
        public string imageP { get; set; }
        public int priceP { get; set; }
        public int sumcount { get; set; }
    }
}