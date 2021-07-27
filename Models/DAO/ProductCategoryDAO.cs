using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class ProductCategoryDAO
    {
        Database db = null;
        public ProductCategoryDAO()
        {
            db = new Database();
        }

        public List<category> ListAll()
        {
            return db.categories.Where(x => x.status == true).OrderBy(x => x.displayOrder).ToList();
        }

        public category ViewDetail(int id)
        {
            return db.categories.Find(id);
        }

    }
}