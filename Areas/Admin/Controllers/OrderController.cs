using Anemone.Common;
using Anemone.Models.DAO;
using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        Database db = new Database();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var modelOrder = db.orders.OrderByDescending(x => x.orderDate).ToList();
            return View(modelOrder);
        }


        
       
        public ActionResult Update(int id)
        {
            
            order sl = db.orders.SingleOrDefault(x => x.idOrder == id);
            if (sl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                var dao = new OrderDAO();
                var res = dao.Update(id);
                /* db.orders.Add(sl);
                 db.SaveChanges();*/
                return RedirectToAction("Index", "Order");
            }
            return View();
            
        }
       


    }
}