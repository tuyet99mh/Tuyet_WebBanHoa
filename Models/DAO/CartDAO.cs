using Anemone.Models.EF;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class CartDAO
    {
        Database db = null;
        public CartDAO()
        {
            db = new Database();
        }
        public bool Delete(int id)
        {
            try
            {
                var pr = db.cart_product.SingleOrDefault(x => x.produID== id);
                db.cart_product.Remove(pr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

       /* public IEnumerable<OrderdetailModel> ListProductType()
        {
            IEnumerable<OrderdetailModel> list = db.orderdetails.;

            return list.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }*/
    }
}