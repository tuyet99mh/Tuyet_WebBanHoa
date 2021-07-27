using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class OrderDAO
    {
        Database db = null;
        public OrderDAO()
        {
            db = new Database();
        }
        public bool Update(int idOr)
        {
            var or = db.orders.SingleOrDefault(n => n.idOrder == idOr);
            try
            {
                /*user.email = entity.email;*/
               /* var ur = db.users.Find(entity.userID);*/

                
                or.status = true;
                /*user.modifiedBy = entity.modifiedBy;*/
               
                /*user.userTypeID = entity.userTypeID;*/


                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }


        }
    }
}