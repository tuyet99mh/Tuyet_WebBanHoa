using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models
{
    public class OrderdetailModel
    {
        public int ido { get; set; }
        public int idpr { get; set; }
        public string imageO { get; set; }
        public string nameO { get; set; }
        public int countO { get; set; }
        public int sumPrice { get; set; }
        public DateTime DateOrder { get; set; }
        public bool status { get; set; }
    }
}