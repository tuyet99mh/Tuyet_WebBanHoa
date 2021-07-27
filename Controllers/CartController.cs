using Anemone.Common;
using Anemone.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session[UserSession.LOGIN_SESSION] == null)
            {
                return RedirectToAction("Home", "Product");
            }
            else
            {
                var dao = new UserDAO();
                var res = dao.getAllCartFromAccount(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()));
                ViewBag.data = res;
                ViewBag.count = res.Count();
            }
            return View();
        }
        [HttpPost]
        public JsonResult delete(int idp)
        {
            var dao = new UserDAO();
            var res = dao.DeleteCart(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()), idp);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getPriceCart()
        {
            var dao = new UserDAO();
            var res = dao.sumPriceCart(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()));
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Order()
        {
            var dao = new UserDAO();
            var res = dao.CreateOrder(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()));
            if (res == null)
            {
                return View("Index");
            }
            else
            {

                var confirm = dao.autoRenderOrder(res);
                if (confirm)
                {
                    return RedirectToAction("Index", "Orderdetail", new { ido = res });
                }
                else
                {
                    return RedirectToAction("Index", "Orderdetail");
                }
            }
        }
/*
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                new CartDAO().Delete(id);

                //return RedirectToAction("Index");
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }

        }*/

        public ActionResult XoaKhoiGio(int SanPhamID)
        {
            try
            {
                new CartDAO().Delete(SanPhamID);

                //return RedirectToAction("Index");
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}