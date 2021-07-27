using Anemone.Common;
using Anemone.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Controllers
{
    public class PartialRenderController : Controller
    {
        // GET: PartialRender
        public ActionResult Index()
        {
            if (Session[UserSession.LOGIN_SESSION] == null)
            {
                ViewBag.IsLogin = "null";
            }
            else
            {
                var dao = new UserDAO();
                ViewBag.IsLogin = dao.getNameByEmail(Session[UserSession.LOGIN_SESSION].ToString()).ToString();
            }
            return PartialView("_Layout");


        }
        public ActionResult HeaderRender()
        {
            if (Session[UserSession.LOGIN_SESSION] == null)
            {
                ViewBag.IsLogin = "null";
            }
            else
            {
                var dao = new UserDAO();
                ViewBag.Count = dao.getCountCart(dao.getIdByEmail(Session[UserSession.LOGIN_SESSION].ToString()));
                ViewBag.IsLogin = dao.getNameByEmail(Session[UserSession.LOGIN_SESSION].ToString()).ToString();
                
            }
            return PartialView("Header");
        }
        

    }
}