using Anemone.Common;
using Anemone.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Controllers
{
    public class OrderdetailController : Controller
    {
        // GET: Orderdetail
        public ActionResult Index(int? ido)
        {
            var req = (ido ?? 0);
            if (Session[UserSession.LOGIN_SESSION] == null)
            {
                return RedirectToAction("Home", "Product");
            }
            var dao = new UserDAO();
            var res = dao.getAllOder(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()), req);
            ViewBag.Data = res;
            ViewBag.Count = res.Count();
            return View();


        }

        public ActionResult OrderDone(int? ido)
        {
            var req = (ido ?? 0);
            if (Session[UserSession.LOGIN_SESSION] == null)
            {
                return RedirectToAction("Home", "Product");
            }
            var dao = new UserDAO();
            var res = dao.getAllOderTrue(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()), req);
            ViewBag.Data = res;
            ViewBag.Count = res.Count();
            return View();


        }


        public ActionResult AllOrder()
        {


           /* string search = "select orders.idOrder , COUNT(orderdetails.proID) AS  from orderdetails where proName like '%" + tensp + "%'";
            var rs = db.products.SqlQuery(search).ToList();*/

            return View();


        }


    }
}