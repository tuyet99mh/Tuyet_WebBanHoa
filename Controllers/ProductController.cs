using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anemone.Common;
using Anemone.Models.DAO;
using Anemone.Models.EF;

namespace Anemone.Controllers
{
    public class ProductController : Controller
    {

        // GET: Product
        public ActionResult Index(string search, int page = 1, int pageSize = 6)
        {
            var dao = new ProductDAO();
           
            var model = dao.ListProductAllNew(search, page, pageSize);
            

            return View(model);
        }

       
        public ActionResult Home()
        {
            var dao = new ProductDAO();
            ViewBag.Top = dao.ListNewProduct(6);
            ViewBag.Minus = dao.GetMinusPrice();
            return View();
        }
        public ActionResult ProductsByProType(int id_cat, int? page, int pageSize = 6)
        {
            var db = new ProductDAO();
            ViewBag.typeName = db.getCatNamebyID(id_cat);
            int pageNumber = (page ?? 1);
            var model = db.ListProductType(id_cat, pageNumber, pageSize );
            ViewBag.typeName = db.getCatNamebyID(id_cat);
            
            return View(model);
        }
        public ActionResult Detail(int id_pr)
        {
            if (id_pr == null)
            {
                return View("Error");
            }
            var dao = new ProductDAO();
            var sp = dao.findByID(id_pr);

            ViewBag.Cat = dao.getPrByCat(id_pr);

            return View(sp);
        }
        [HttpPost]
        public JsonResult addToCart(int id, int count)
        {
            try
            {
                if (Session[UserSession.LOGIN_SESSION] == null)
                {
                    return Json(new { res = 0 }, JsonRequestBehavior.AllowGet);
                }
                string email = Session[UserSession.LOGIN_SESSION].ToString();
                string name = Session[UserSession.LOGIN_SESSION].ToString();
                var dao = new UserDAO();
                var res = dao.addToCart(dao.getIdByEmail(email), id, count);
                if (res == 1)
                {
                    return Json(new { res = 1 }, JsonRequestBehavior.AllowGet);
                }
                else if (res == 0)
                {
                    return Json(new { res = 2 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { res = -1 }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { res = -2 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult getCount()
        {
            var dao = new UserDAO();
            var count = dao.getCountCart(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()));
            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }
        
    }
}