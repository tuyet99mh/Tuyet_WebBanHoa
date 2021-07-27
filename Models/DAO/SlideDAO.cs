using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class SlideDAO
    {

        Database db = null;
        public SlideDAO()
        {
            db = new Database();
        }
        public IEnumerable<slide> ListSlide()
        {
            IEnumerable<slide> list = db.slides.Where(p => p.status == true);

            return list;
        }
    }
}